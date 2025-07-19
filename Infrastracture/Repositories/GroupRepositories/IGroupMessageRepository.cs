using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IGroupMessageRepository : IGenericRepository<GroupMessage>
{
    Task<List<GroupMessage>> GetMessagesByGroupChatIdAsync(Guid groupChatId);
    Task<GroupMessage?> GetMessageByIdAsync(Guid messageId);
    Task EditMessageAsync(Guid messageId, Guid userId, string newText);
    Task DeleteMessagesByGroupIdAsync(Guid groupChatId);
    Task SaveChangesAsync();
}