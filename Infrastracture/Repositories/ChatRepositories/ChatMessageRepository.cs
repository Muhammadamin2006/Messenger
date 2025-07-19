using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class ChatMessageRepository : GenericRepository<ChatMessage>, IChatMessageRepository
{
    private readonly MessengerContext _context;

    public ChatMessageRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId)
    {
        return await _context.ChatMessages
            .Where(m => m.ChatId == chatId)
            .Include(m => m.Sender) 
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task<ChatMessage?> GetMessageByIdAsync(Guid messageId)
    {
        return await _context.ChatMessages
            .Include(m => m.Sender)
            .FirstOrDefaultAsync(m => m.MessageId == messageId);
    }

    public async Task EditMessageAsync(Guid messageId, Guid userId, string newText)
    {
        var message = await _context.ChatMessages
            .FirstOrDefaultAsync(m => m.MessageId == messageId);

        if (message == null)
            throw new Exception("Сообщение не найдено");

        if (message.SenderId != userId)
            throw new Exception("Редактировать можно только свои сообщения");

        message.Text = newText;
        message.EditedAt = DateTime.UtcNow;

        _context.ChatMessages.Update(message);
        await _context.SaveChangesAsync();
    }
    
    

    public async Task DeleteMessagesByChatIdAsync(Guid chatId)
    {
        var messages = await _context.ChatMessages
            .Where(m => m.ChatId == chatId)
            .ToListAsync();

        _context.ChatMessages.RemoveRange(messages);
        await _context.SaveChangesAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
