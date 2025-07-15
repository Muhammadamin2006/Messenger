using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IGroupUserRepository : IGenericRepository<GroupUser>
{
    Task<bool> IsUserInGroupAsync(Guid userId, Guid groupId);
    Task<List<GroupUser>> GetGroupUsersAsync(Guid groupId);
    Task SaveChangesAsync();
}