using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IGroupMessageVisibilityRepository : IGenericRepository<GroupMessageVisibility>
{
    Task<bool> IsMessageHiddenForUserAsync(Guid messageId, Guid userId);
    Task<List<Guid>> GetHiddenMessageIdsForUserAsync(Guid userId);

    Task SaveChangesAsync();
}