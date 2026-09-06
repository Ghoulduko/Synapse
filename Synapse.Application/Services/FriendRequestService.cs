using Synapse.Application.Dtos;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Application.Interfaces.ServiceInterfaces;
using Synapse.Core.Entities;
using Synapse.Core.Enums;
using Synapse.Core.Models;

namespace Synapse.Application.Services;

public class FriendRequestService : IFriendRequestService
{
    private readonly IFriendRequestRepository _friendRequestepository;

    public FriendRequestService(IFriendRequestRepository repository)
    {
        _friendRequestepository = repository;
    }

    public async Task<IEnumerable<UserDto>> GetUserFriendRequestSenderAsync(int userId)
    {
        var senders = await _friendRequestepository.GetAllUserFriendRequestsSenders(userId);

        return senders.Select(u => new UserDto
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
        });
    }

    public async Task<Result<FriendRequestDto>> CreateFriendRequest(int senderId, int receiverId)
    {
        if (senderId == receiverId)
        {
            return new Result<FriendRequestDto>
            {
                Success = false,
                Message = "You cannot send friend request to yourself."
            };
        }
        
        var friendRequestInDb = await _friendRequestepository.GetFriendRequest(senderId, receiverId);
        
        if (friendRequestInDb != null && friendRequestInDb.Status != FriendRequestStatus.Accepted)
        {
            return new Result<FriendRequestDto>
            {
                Success = false,
                Message = "Friend request already exists."
            };
        }
        
        if (friendRequestInDb != null && friendRequestInDb.Status == FriendRequestStatus.Accepted)
        {
            return new Result<FriendRequestDto>
            {
                Success = false,
                Message = "You are already friends."
            };
        }
        
        var otherGuysRequestToUser = await _friendRequestepository.GetFriendRequest(receiverId, senderId);

        if (otherGuysRequestToUser != null)
        {
            return new Result<FriendRequestDto>
            {
                Success = false,
                Message = "Friend request already exists."
            };
        }
        
        var friendRequest = new FriendRequest(senderId, receiverId);
        await _friendRequestepository.CreateFriendRequest(friendRequest);

        return new Result<FriendRequestDto>
        {
            Success = true,
            Message = "Friend request created successfully"
        };
    }

    public async Task<Result<FriendRequestDto>> UpdateFriendRequest(int senderId, int receiverId)
    {
        var friendRequest = await _friendRequestepository.GetFriendRequest(senderId, receiverId);
        if (friendRequest == null)
        {
            return new Result<FriendRequestDto>
            {
                Success = false,
                Message = "Friend request not found."
            };
        }
        
        friendRequest.Status = FriendRequestStatus.Accepted;
        await _friendRequestepository.SaveChangesAsync();

        return new Result<FriendRequestDto>
        {
            Success = true,
            Message = "Friend request accepted successfully"
        };
    }

    public async Task<Result<FriendRequestDto>> DeclineFriendRequest(int senderId, int receiverId)
    {
        var friendRequest = await _friendRequestepository.GetFriendRequest(senderId, receiverId);
        if (friendRequest == null)
        {
            return new Result<FriendRequestDto>
            {
                Success = false,
                Message = "Friend request not found."
            };
        }

        if (friendRequest.Status == FriendRequestStatus.Accepted)
        {
            return new Result<FriendRequestDto>
            {
                Success = true,
                Message = "The friend request is already accepted."
            };
        } 
        
        await _friendRequestepository.DeclineFriendRequest(friendRequest);
        return new Result<FriendRequestDto>
        {
            Success = true,
            Message = "Friend request declined successfully"
        };
        
    }
}