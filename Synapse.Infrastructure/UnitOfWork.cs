using Microsoft.EntityFrameworkCore;
using Synapse.Application.Interfaces;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Core.Entities;
using Synapse.Core.Enums;
using Synapse.Infrastructure.Database;

namespace Synapse.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly SynapseDbContext _context;
    public IUserRepository UserRepository { get; }
    public IFriendRequestRepository FriendRequestRepository { get; }
    
    public UnitOfWork(SynapseDbContext context, IUserRepository userRepository, IFriendRequestRepository friendRequestRepository)
    {
        _context = context;
        UserRepository = userRepository;
        FriendRequestRepository = friendRequestRepository;
    }
}