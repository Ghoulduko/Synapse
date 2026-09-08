using Synapse.Core.Entities;

namespace Synapse.Application.Interfaces.RepositoryInterfaces;

public interface IMessageRepository
{
    Task AddMessageAsync(Message message);
    Task<Message?> GetMessageByIdAsync(int id);
    Task<List<Message>> GetConversationMessages(int conversationId);
}