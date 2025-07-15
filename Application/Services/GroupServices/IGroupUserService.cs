using Messenger.Application.Dtos.GroupDto;

namespace Messenger.Application.Services.GroupServices;

public interface IGroupUserService
{
    Task<bool> AddUserToGroupAsync(AddGroupUserDto dto);
}