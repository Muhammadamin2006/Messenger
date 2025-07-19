using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Application.Dtos.UserDtos;
using Messenger.Domain.Models;
using Messenger.Domain.Models.UserModels;

namespace Messenger.Infrastracture.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<RegistrationDto, User>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
        
    }

}