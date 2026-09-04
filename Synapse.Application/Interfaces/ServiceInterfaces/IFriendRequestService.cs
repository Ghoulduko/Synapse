using Synapse.Application.Dtos;
using Synapse.Core.Models;

namespace Synapse.Application.Interfaces.ServiceInterfaces;

public interface IFriendRequestService
{
    Task<IEnumerable<UserDto>> GetUserFriendRequestSenderAsync(int userId);
    Task<Result<FriendRequestDto>> CreateFriendRequest(int senderId, int receiverId);
    Task<Result<FriendRequestDto>> UpdateFriendRequest(int senderId, int receiverId);
    Task<Result<FriendRequestDto>> DeleteFriendRequest(int friendRequestId);
}