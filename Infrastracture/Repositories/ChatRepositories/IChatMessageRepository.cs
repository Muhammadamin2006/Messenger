using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IChatMessageRepository : IGenericRepository<ChatMessage>
{
    Task<List<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId);

    Task<ChatMessage?> GetMessageByIdAsync(Guid messageId);

    Task EditMessageAsync(Guid messageId, Guid userId, string newText);
    
    Task DeleteMessagesByChatIdAsync(Guid chatId);
    
    Task SaveChangesAsync();
}

