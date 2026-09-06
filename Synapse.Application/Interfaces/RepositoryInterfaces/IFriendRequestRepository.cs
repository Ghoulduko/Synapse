using Synapse.Core.Entities;

namespace Synapse.Application.Interfaces.RepositoryInterfaces;

public interface IFriendRequestRepository
{
    Task CreateFriendRequest(FriendRequest friendRequestDto);
    Task<FriendRequest?> GetFriendRequest(int senderId, int receiverId);
    Task DeclineFriendRequest(FriendRequest friendRequest);
    Task<IEnumerable<User>> GetAllUserFriends(int userId);
    Task<IEnumerable<User>> GetAllUserFriendRequestsSenders(int receiverUserId);
    Task SaveChangesAsync();
}