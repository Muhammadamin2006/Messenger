using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Chat, ChatDto>()
            .ForMember(dest => dest.FirstUserName, opt => opt.MapFrom(src => src.FirstUser.Username))
            .ForMember(dest => dest.SecondUserName, opt => opt.MapFrom(src => src.SecondUser.Username));
    }

}