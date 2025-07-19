using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories.ChatRepositories;

public interface IChatUserRepository : IGenericRepository<ChatUser>
{
    Task<List<ChatUser>> GetUsersInChatAsync(Guid chatId);
    Task<bool> IsUserInChatAsync(Guid userId, Guid chatId);
    Task RemoveUserFromChatAsync(Guid userId, Guid chatId);
    Task RemoveAllUsersFromChatAsync(Guid chatId);
    Task<List<Guid>> GetUserIdsInChatAsync(Guid chatId);
    Task SaveChangesAsync();
}