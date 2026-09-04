using Microsoft.EntityFrameworkCore;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Core.Entities;
using Synapse.Core.Enums;
using Synapse.Infrastructure.Database;

namespace Synapse.Infrastructure.Repositories;

public class FriendRequestRepository : IFriendRequestRepository
{
    private readonly SynapseDbContext _context;

    public FriendRequestRepository(SynapseDbContext context)
    {
        _context = context;
    }

    public async Task CreateFriendRequest(FriendRequest friendRequestDto)
    {
        _context.FriendRequests.Add(friendRequestDto);
        await _context.SaveChangesAsync();
    }

    public async Task<FriendRequest?> GetFriendRequest(int senderId, int receiverId)
    {
        return await _context.FriendRequests.SingleOrDefaultAsync(r => r.SenderId == senderId && r.ReceiverId == receiverId);
    }

    public async Task<FriendRequest?> GetFriendRequestByUserId(int userId)
    {
        return await _context.FriendRequests.SingleOrDefaultAsync(r => 
            r.ReceiverId == userId || r.SenderId == userId);
    }

    public async Task DeclineFriendRequest(FriendRequest friendRequest)
    {
        _context.FriendRequests.Remove(friendRequest);
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<User>> GetAllUserFriends(int userId)
    {
        var acceptedFriendRequests = await _context.FriendRequests.Where(r => 
            (r.ReceiverId == userId || r.SenderId == userId) && r.Status == FriendRequestStatus.Accepted).ToListAsync();

        List<int> friendIds = new List<int>();
        foreach (var friendRequest in acceptedFriendRequests)
        {
            if (friendRequest.ReceiverId == userId)
                friendIds.Add(friendRequest.SenderId);
            else if (friendRequest.SenderId == userId)
                friendIds.Add(friendRequest.ReceiverId);
        }
        
        var friends = await _context.Users
            .Where(u => friendIds.Contains(u.Id))
            .ToListAsync();
        
        return friends;
    }

    public async Task<IEnumerable<User>> GetAllUserFriendRequestsSenders(int receiverUserId)
    {
        var friendRequests = await _context.FriendRequests.Where(r => r.ReceiverId == receiverUserId && r.Status == FriendRequestStatus.Pending).ToListAsync();
        var senderIds = friendRequests.Select(r => r.SenderId);
        return await _context.Users.Where(u => senderIds.Contains(u.Id)).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}