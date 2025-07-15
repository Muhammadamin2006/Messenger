using Messenger.Application.Dtos;
using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IChatRepository : IGenericRepository<Chat>
{
    Task<Chat?> GetChatBetweenUsersAsync(Guid firstUserId, Guid secondUserId);
    Task<List<Chat>> GetChatsForUserAsync(Guid userId);
    Task SaveChangesAsync();
}