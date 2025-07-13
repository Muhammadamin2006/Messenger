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
    }

}