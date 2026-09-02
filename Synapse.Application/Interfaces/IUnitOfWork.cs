using Synapse.Application.Interfaces.RepositoryInterfaces;

namespace Synapse.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IFriendRequestRepository FriendRequestRepository { get; }
}