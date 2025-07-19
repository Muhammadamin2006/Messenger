using AutoMapper;
using Messenger.Application.Dtos.GroupDto;
using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Mappers;

public class GroupMappers : Profile
{
    public GroupMappers()
    {
        CreateMap<AddGroupUserDto, GroupUser>();
        CreateMap<CreateGroupMessageDto, GroupMessage>();
        CreateMap<GroupMessage, GroupMessageDto>()
            .ForMember(dest => dest.SenderName, 
                opt => 
                    opt.MapFrom(src => src.Sender.Username));
    }
}