using Synapse.Core.Entities;

namespace Synapse.Application.Interfaces.RepositoryInterfaces;

public interface IConversationRepository
{
    Task CreateConversation(Conversation conversation);
    Task<Conversation?> GetConversationById(int id);
    Task<List<Conversation>> GetConversationsOfUser(int userId);
}