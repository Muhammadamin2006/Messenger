using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Application.Dtos.ChatDtos;
using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Mappers.ChatMappers;

public class ChatMapper : Profile
{
    public ChatMapper()
    {
        CreateMap<Chat, ChatDto>()
            .ForMember(dest => dest.FirstUserName, opt => opt.MapFrom(src => src.FirstUser.Username))
            .ForMember(dest => dest.SecondUserName, opt => opt.MapFrom(src => src.SecondUser.Username));
    }
}