using AutoMapper;
using Messenger.Application.Dtos.GroupDto;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Repositories;

namespace Messenger.Application.Services.GroupServices;

public class GroupUserService : IGroupUserService
{
    private readonly IGroupUserRepository _groupUserRepository;
    private readonly IMapper _mapper;

    public GroupUserService(IGroupUserRepository groupUserRepository, IMapper mapper)
    {
        _groupUserRepository = groupUserRepository;
        _mapper = mapper;
    }

    public async Task<bool> AddUserToGroupAsync(AddGroupUserDto dto)
    {
        var alreadyInGroup = await _groupUserRepository.IsUserInGroupAsync(dto.UserId, dto.GroupId);
        if (alreadyInGroup)
            return false;

        var groupUser = _mapper.Map<GroupUser>(dto);
        groupUser.GroupUserId = Guid.NewGuid();
        groupUser.JoinedAt = DateTime.UtcNow;

        await _groupUserRepository.AddAsync(groupUser);
        await _groupUserRepository.SaveChangesAsync();

        return true;
    }
}