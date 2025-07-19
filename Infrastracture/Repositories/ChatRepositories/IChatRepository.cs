using Messenger.Application.Dtos;
using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IChatRepository : IGenericRepository<Chat>
{
    Task<Chat?> GetChatByIdAsync(Guid chatId);
    Task<Chat?> GetChatBetweenUsersAsync(Guid firstUserId, Guid secondUserId);
    Task<List<Chat>> GetChatsForUserAsync(Guid userId);
    Task<bool> ChatExistsAsync(Guid user1Id, Guid user2Id);
    Task DeleteChatCompletelyAsync(Guid chatId);
    Task SaveChangesAsync(Chat chat);
}