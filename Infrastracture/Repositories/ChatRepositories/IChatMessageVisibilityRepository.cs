using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IChatMessageVisibilityRepository : IGenericRepository<ChatMessageVisibility>
{
    Task<bool> IsMessageHiddenForUserAsync(Guid messageId, Guid userId);
    Task<List<Guid>> GetHiddenMessageIdsAsync(Guid userId, Guid chatId);
    Task SaveChangesAsync();
}
