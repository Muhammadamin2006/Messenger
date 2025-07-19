using Messenger.Application.Dtos;
using Messenger.Application.Dtos.GroupDto;
using Messenger.Application.Dtos.UserDtos;
using Messenger.Domain.Models;
using SearchContactDto = Messenger.Application.Dtos.SearchContactDto;

namespace Messenger.Application.Services;

public interface IUserService
{
    Task<UserDto> RegisterUserAsync(RegistrationDto dto);
    Task<bool> IsEmailTakenAsync(string email);
    Task<List<SearchContactDto>> SearchUsersByNameAsync(string name);
    Task DeleteUserAsync(Guid userId);
    Task<List<UserDto>> GetAllUsersAsync();
    Task<List<GroupDto>> GetUserGroupsAsync(Guid userId);

}