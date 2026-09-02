using Synapse.Core.Entities;

namespace Synapse.Application.Interfaces.RepositoryInterfaces;

public interface IFriendRequestRepository
{
    Task CreateFriendRequest(FriendRequest friendRequestDto);
    Task<FriendRequest?> GetFriendRequestById(int userId);
    Task DeclineFriendRequest(FriendRequest friendRequest);
    Task<IEnumerable<User>> GetAllUserFriends(int userId);
}