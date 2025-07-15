using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class ChatMessageRepository : GenericRepository<ChatMessage>, IChatMessageRepository
{
    private MessengerContext _context;
    
    public ChatMessageRepository(MessengerContext context) : base(context)
    {
        _context = context;
        
    }

    public async Task<List<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId)
    {
        return await _dbSet
            .Where(m => m.ReceivedChatId == chatId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task<ChatMessage?> GetMessageByIdAsync(Guid messageId)
    {
        return await _dbSet.FirstOrDefaultAsync(m => m.MessageId == messageId);
    }

    public async Task SaveChangesAsync(ChatMessage chatMessage)
    {
        await _context.SaveChangesAsync();
    }
}
