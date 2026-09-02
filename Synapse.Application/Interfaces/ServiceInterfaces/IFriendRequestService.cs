using Synapse.Application.Dtos;
using Synapse.Core.Models;

namespace Synapse.Application.Interfaces.ServiceInterfaces;

public interface IFriendRequestService
{
    Task<Result<FriendRequestDto>> CreateFriendRequest(FriendRequestDto friendRequestDto);
    Task<Result<FriendRequestDto>> UpdateFriendRequest(FriendRequestDto friendRequestDto);
    Task<Result<FriendRequestDto>> DeleteFriendRequest(int friendRequestId);
}