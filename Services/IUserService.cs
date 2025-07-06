using Messenger.DTOs;
using Messenger.Infrastractures.Database;
using Messenger.Models;


namespace Messenger.Services;

public interface IUserService
{
    IQueryable GetAll(UserDto userDto);
    
    UserDto GetById(Guid id);
    
    bool TryUpdate(UpdateUserDto updateUserDto);
    
    bool Add(CreateUserDto createUserDto);
    
    bool TryDelete(Guid userid);
    
    void SaveChanges();

    
}