using Microsoft.EntityFrameworkCore;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Core.Entities;
using Synapse.Infrastructure.Database;

namespace Synapse.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly SynapseDbContext _context;

    public MessageRepository(SynapseDbContext context)
    {
        _context = context;
    }

    public async Task AddMessageAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    public async Task<Message?> GetMessageByIdAsync(int id)
    {
        return await _context.Messages.FindAsync(id);
    }

    public async Task<List<Message>> GetConversationMessages(int conversationId)
    {
        return await _context.Messages.Where(m => m.ConversationId == conversationId).ToListAsync();
    }
}