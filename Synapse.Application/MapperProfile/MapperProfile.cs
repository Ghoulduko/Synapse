using AutoMapper;
using Synapse.Application.Dtos;
using Synapse.Application.Dtos.Chat;
using Synapse.Core.Entities;

namespace Synapse.Application.MapperProfile;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<ConversationDto, Conversation>().ReverseMap();
        CreateMap<ConversationParticipantDto, ConversationParticipant>().ReverseMap();
        CreateMap<MessageDto, Message>().ReverseMap();
    }
}