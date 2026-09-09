using AutoMapper;
using Synapse.Application.Dtos.Chat;
using Synapse.Application.Interfaces;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Application.Interfaces.ServiceInterfaces;
using Synapse.Core.Entities;
using Synapse.Core.Models;

namespace Synapse.Application.Services;

public class ConversationService : IConversationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public ConversationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ConversationDto>> CreateConversation(List<int> userIds, int userId)
    {
        if (userIds.Count < 1)
        {
            return new Result<ConversationDto>
            {
                Success = false,
                Message = "Cannot create a conversation for less than 2 people."
            };
        }
        
        userIds.Add(userId);
        var users = await _unitOfWork.UserRepository.GetManyUserWithIds(userIds);

        if (users.Any(u => u == null))
        {
            return new Result<ConversationDto>
            {
                Success = false,
                Message = "one or more account was not found."
            };
        } 
        
        var participants = new List<ConversationParticipant>();
        
        foreach (var user in users)
        {
            participants.Add(new ConversationParticipant
                {
                    UserId = user.Id,
                }
            );
        }

        var conversation = new Conversation
        {
            CreatedAt = DateTime.UtcNow,
            Participants = participants,
        };
        
        await _unitOfWork.ConversationRepository.CreateConversation(conversation);
        
        return new Result<ConversationDto>
        {
            Success = true,
            Data = _mapper.Map<ConversationDto>(conversation)
        };
    }

    public async Task<Result<ConversationDto>> GetConversationById(int id)
    {
        var conversation = await _unitOfWork.ConversationRepository.GetConversationById(id);
        if (conversation == null)
        {
            return new Result<ConversationDto>
            {
                Success = false,
                Message = "Conversation not found."
            };
        }

        return new Result<ConversationDto>
        {
            Success = true,
            Data = _mapper.Map<ConversationDto>(conversation)
        };
    }

    public async Task<List<ConversationDto>> GetConversationsOfUser(int userId)
    {
        var conversations =  await _unitOfWork.ConversationRepository.GetConversationsOfUser(userId);
        return _mapper.Map<List<ConversationDto>>(conversations);
    }
}