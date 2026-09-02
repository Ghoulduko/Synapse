using Synapse.Application.Dtos;
using Synapse.Application.Interfaces.ServiceInterfaces;
using Synapse.Core.Models;

namespace Synapse.Application.Services;

public class FriendRequestService : IFriendRequestService
{
    public Task<Result<FriendRequestDto>> CreateFriendRequest(FriendRequestDto friendRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<FriendRequestDto>> UpdateFriendRequest(FriendRequestDto friendRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<FriendRequestDto>> DeleteFriendRequest(int friendRequestId)
    {
        throw new NotImplementedException();
    }
}