using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IUserBlocksRepository : IGenericRepository<User>
{
    Task BlockUserAsync(Guid blockerId, Guid blockedId); // барои блок кадан
    Task UnblockUserAsync(Guid blockerId, Guid blockedId); // анблок каданба
    Task<bool> IsBlockedAsync(Guid blockerId, Guid blockedId);  // заблок кадагими е неми
    Task<List<User>> GetBlockedUsersAsync(Guid blockerId); // заблок кадагихоя гирон метияд
}