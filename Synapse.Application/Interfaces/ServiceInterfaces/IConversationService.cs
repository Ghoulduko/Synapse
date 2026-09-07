using Synapse.Application.Dtos;
using Synapse.Application.Dtos.Chat;
using Synapse.Core.Entities;
using Synapse.Core.Models;

namespace Synapse.Application.Interfaces.ServiceInterfaces;

public interface IConversationService
{
    Task<Result<ConversationDto>> CreateConversation(List<int> userIds);
    Task<Result<ConversationDto>> GetConversationById(int id);
    Task<List<ConversationDto>> GetConversationsOfUser(int userId);
}