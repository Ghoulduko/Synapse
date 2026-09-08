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

    public async Task<Result<MessageDto>> AddMessageAsync(AddMessageDto message)
    {
        if (message.SenderId < 1 || message.ConversationId < 1)
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

        var newMessage = new Message
        {
            ConversationId = message.ConversationId,
            SenderId = message.SenderId,
            Content = message.Content,
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

    public async Task<IEnumerable<MessageDto>> GetConversationMessages(int conversationId)
    {
        var conversation = await _conversationRepository.GetConversationById(conversationId);
        if (conversation == null)
        {
            return null;
        }

        var messages = _mapper.Map<List<MessageDto>>(conversation.Messages);
        return messages;
    }
}