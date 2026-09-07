using Microsoft.EntityFrameworkCore;
using Synapse.Application.Interfaces;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Core.Entities;
using Synapse.Core.Enums;
using Synapse.Infrastructure.Database;

namespace Synapse.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IFriendRequestRepository FriendRequestRepository { get; }
    public IConversationRepository ConversationRepository { get; }
    
    public UnitOfWork(
        IUserRepository userRepository, 
        IFriendRequestRepository friendRequestRepository,
        IConversationRepository conversationRepository)
    {
        UserRepository = userRepository;
        FriendRequestRepository = friendRequestRepository;
        ConversationRepository = conversationRepository;
    }
}