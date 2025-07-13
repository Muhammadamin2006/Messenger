using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<bool> IsEmailTakenAsync(string email);
    Task<User?> GetByIdAsync(Guid userId); 
    Task DeleteAsync(User user);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<List<User>> GetAllUsersAsync();
}