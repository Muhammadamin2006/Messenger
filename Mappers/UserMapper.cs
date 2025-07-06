using Messenger.DTOs;
using Messenger.Models;

namespace Messenger.Mappers;
using AutoMapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>();
        CreateMap<UpdateUserDto, User>();
       
        CreateMap<UserDto, User>();
        CreateMap<User, UpdateUserDto>();
        
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(_ => Guid.NewGuid()));

    }
    
    
}