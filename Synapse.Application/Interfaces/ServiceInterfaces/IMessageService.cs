using Synapse.Application.Dtos.Chat;
using Synapse.Core.Models;

namespace Synapse.Application.Interfaces.ServiceInterfaces;

public interface IMessageService
{
    Task<Result<MessageDto>> AddMessageAsync(AddMessageDto message);
    Task<Result<MessageDto>> GetMessageByIdAsync(int id);
    Task<IEnumerable<MessageDto>> GetConversationMessages(int conversationId);
}