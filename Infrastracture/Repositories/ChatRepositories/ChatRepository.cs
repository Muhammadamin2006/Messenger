using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories.ChatRepositories;

public class ChatRepository : GenericRepository<Chat>, IChatRepository
{
    private readonly MessengerContext _context;

    public ChatRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Chat?> GetChatByIdAsync(Guid chatId)
    {
        return await _context.Chats
            .Include(c => c.ChatUsers)
            .Include(c => c.ChatMessages)
            .FirstOrDefaultAsync(c => c.ChatId == chatId);
    }

    public async Task<Chat?> GetChatBetweenUsersAsync(Guid firstUserId, Guid secondUserId)
    {
        return await _context.Chats
            .Include(c => c.ChatUsers)
            .FirstOrDefaultAsync(c =>
                c.ChatUsers.Any(u => u.UserId == firstUserId) &&
                c.ChatUsers.Any(u => u.UserId == secondUserId));
    }

    public async Task<List<Chat>> GetChatsForUserAsync(Guid userId)
    {
        return await _context.Chats
            .Include(c => c.ChatUsers)
            .Where(c => c.ChatUsers.Any(u => u.UserId == userId))
            .ToListAsync();
    }

    public async Task<bool> ChatExistsAsync(Guid user1Id, Guid user2Id)
    {
        return await _context.Chats
            .AnyAsync(c =>
                c.ChatUsers.Any(u => u.UserId == user1Id) &&
                c.ChatUsers.Any(u => u.UserId == user2Id));
    }

    public async Task DeleteChatCompletelyAsync(Guid chatId)
    {
        var chat = await _context.Chats
            .Include(c => c.ChatUsers)
            .Include(c => c.ChatMessages)
            .FirstOrDefaultAsync(c => c.ChatId == chatId);

        if (chat != null)
        {
            _context.ChatMessages.RemoveRange(chat.ChatMessages);

            _context.ChatUsers.RemoveRange(chat.ChatUsers);

            _context.Chats.Remove(chat);

            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync(Chat chat)
    {
        await _context.SaveChangesAsync();
    }

  
}