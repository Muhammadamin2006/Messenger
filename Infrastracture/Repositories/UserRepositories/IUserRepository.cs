using Messenger.Domain.Models;
using Messenger.Domain.Models.UserModels;

namespace Messenger.Infrastracture.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task AddAsync(User user);
    Task<bool> IsEmailTakenAsync(string email);
    Task<List<User>> SearchUsersByNameAsync(string name);
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetByEmailAsync(string email);

}