using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Mappers;

public class BlockUsersMapper : Profile
{
    public BlockUsersMapper()
    {
        CreateMap<BlockUserDto, UserBlock>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BlockedAt, opt => opt.Ignore());

        CreateMap<UserBlock, BlockUserDto>();
    }
}
