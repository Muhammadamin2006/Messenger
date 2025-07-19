using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IGroupMessageVisibilityRepository : IGenericRepository<GroupMessageVisibility>
{
    Task<bool> IsMessageHiddenForUserAsync(Guid messageId, Guid userId);
    Task<List<Guid>> GetHiddenMessageIdsAsync(Guid userId, Guid groupChatId);
    Task HideMessageForUserAsync(Guid messageId, Guid userId);
    Task DeleteAllVisibilityRecordsForMessageAsync(Guid messageId);
    Task SaveChangesAsync();
}
