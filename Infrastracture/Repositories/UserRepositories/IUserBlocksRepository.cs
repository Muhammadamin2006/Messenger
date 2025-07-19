using Messenger.Domain.Models;
using Messenger.Domain.Models.UserModels;

namespace Messenger.Infrastracture.Repositories;

public interface IUserBlocksRepository : IGenericRepository<User>
{
    Task BlockUserAsync(Guid blockerId, Guid blockedId); 
    Task UnblockUserAsync(Guid blockerId, Guid blockedId); 
    Task<bool> IsBlockedAsync(Guid blockerId, Guid blockedId);  
    Task<List<User>> GetBlockedUsersAsync(Guid blockerId); 
}