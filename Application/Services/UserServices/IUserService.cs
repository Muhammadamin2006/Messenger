using Messenger.Application.Dtos;
using Messenger.Domain.Models;

namespace Messenger.Application.Services;

public interface IUserService
{
    Task<UserDto> RegisterUserAsync(RegisterUserDto dto);
    
    
    Task DeleteUserAsync(Guid userId);
    Task<SearchContactDto?> FindUserByPhoneNumberAsync(string phoneNumber);
    Task<List<UserDto>> GetAllUsersAsync();


}