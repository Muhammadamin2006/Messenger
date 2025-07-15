using Messenger.Domain.Models;

namespace Messenger.Application.Services;

public interface IChatMessageService
{
    Task<ChatMessage> SendMessageAsync(Guid chatId, Guid senderId, string text);
    Task<List<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId, Guid forUserId);
    Task<ChatMessage?> GetMessageByIdAsync(Guid messageId,  Guid forUserId);
    Task<ChatMessage?> EditMessageAsync(Guid messageId, Guid userId, string newText);
    
}