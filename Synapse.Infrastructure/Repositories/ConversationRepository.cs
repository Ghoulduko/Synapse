using Microsoft.EntityFrameworkCore;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Core.Entities;
using Synapse.Infrastructure.Database;

namespace Synapse.Infrastructure.Repositories;

public class ConversationRepository : IConversationRepository
{
    private readonly SynapseDbContext _context;

    public ConversationRepository(SynapseDbContext context)
    {
        _context = context;
    }

    private IQueryable<Conversation> BaseQuery()
    {
        return _context.Conversations
            .Include(c => c.Participants)
                .ThenInclude(p => p.User)
            .Include(c => c.Messages)
                .ThenInclude(m => m.Sender);
    } 

    public async Task CreateConversation(Conversation conversation)
    {
        await _context.Conversations.AddAsync(conversation);
        await _context.SaveChangesAsync();
    }

    public async Task<Conversation?> GetConversationById(int id)
    {
        return await BaseQuery().SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Conversation>> GetConversationsOfUser(int userId)
    {
        return await BaseQuery().Where(c => c.Participants.Any(p => p.UserId == userId)).ToListAsync();
    }
}