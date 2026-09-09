using AutoMapper;
using Synapse.Application.Dtos.Chat;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Application.Interfaces.ServiceInterfaces;
using Synapse.Core.Entities;
using Synapse.Core.Models;

namespace Synapse.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IMapper _mapper;

    public MessageService(IMessageRepository messageRepository, IConversationRepository conversationRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _conversationRepository = conversationRepository;
        _mapper = mapper;
    }

    public async Task<Result<MessageDto>> AddMessageAsync(AddMessageDto message, int senderId)
    {
        if (senderId < 1 || message.ConversationId < 1)
        {
            return new Result<MessageDto>
            {
                Success = false,
                Message = "Invalid Credentials"
            };
        }

        if (message.Content.Length < 1)
        {
            return new Result<MessageDto>
            {
                Success = false,
                Message = "Invalid Content"
            };
        }
        
        var conversation = await _conversationRepository.GetConversationById(message.ConversationId);

        if (conversation == null)
        {
            return new Result<MessageDto>
            {
                Success = false,
                Message = "Conversation not found"
            };
        }

        if (conversation.Participants.All(p => p.UserId != senderId))
        {
            return new Result<MessageDto>
            {
                Success = false,
                Message = "User not in the conversation"
            };
        }

        var newMessage = new Message
        {
            ConversationId = message.ConversationId,
            SenderId = senderId,
            Content = message.Content,
            SentAt = DateTime.UtcNow,
        };

        await _messageRepository.AddMessageAsync(newMessage);

        return new Result<MessageDto>
        {
            Success = true,
            Message = "Message added successfully"
        };
    }

    public async Task<Result<MessageDto>> GetMessageByIdAsync(int id)
    {
        var message = await _messageRepository.GetMessageByIdAsync(id);
        if (message == null)
        {
            return new Result<MessageDto>
            {
                Success = false,
                Message = "Message not found"
            };
        }
        
        return new Result<MessageDto>
        {
            Success = true,
            Data = new MessageDto()
            {
                Id = message.Id,
                ConversationId = message.ConversationId,
                SenderId = message.SenderId,
                Content = message.Content,
                SentAt = message.SentAt,
            }
        };
    }

    public async Task<IEnumerable<MessageDto>> GetConversationMessages(int conversationId, int senderId)
    {
        var conversation = await _conversationRepository.GetConversationById(conversationId);
        if (conversation == null)
        {
            return null;
        }

        if (conversation.Participants.All(p => p.UserId != senderId))
        {
            throw new UnauthorizedAccessException("You are not authorized to access Messages of this Conversation");
        }

        var messages = _mapper.Map<List<MessageDto>>(conversation.Messages);
        return messages;
    }
}