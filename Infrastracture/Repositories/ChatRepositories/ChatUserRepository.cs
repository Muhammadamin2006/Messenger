using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories.ChatRepositories;

public class ChatUserRepository : GenericRepository<ChatUser>, IChatUserRepository
{
    private readonly MessengerContext _context;

    public ChatUserRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ChatUser>> GetUsersInChatAsync(Guid chatId)
    {
        return await _context.ChatUsers
            .Include(cu => cu.User) 
            .Include(cu => cu.Chat) 
            .Where(cu => cu.ChatId == chatId)
            .ToListAsync();
    }
    
    public async Task<bool> IsUserInChatAsync(Guid userId, Guid chatId)
    {
        return await _context.ChatUsers
            .AnyAsync(cu => cu.UserId == userId && cu.ChatId == chatId);
    }

    public async Task RemoveUserFromChatAsync(Guid userId, Guid chatId)
    {
        var chatUser = await _context.ChatUsers
            .FirstOrDefaultAsync(cu => cu.UserId == userId && cu.ChatId == chatId);

        if (chatUser != null)
        {
            _context.ChatUsers.Remove(chatUser);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllUsersFromChatAsync(Guid chatId)
    {
        var chatUsers = await _context.ChatUsers
            .Where(cu => cu.ChatId == chatId)
            .ToListAsync();

        _context.ChatUsers.RemoveRange(chatUsers);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Guid>> GetUserIdsInChatAsync(Guid chatId)
    {
        return await _context.ChatUsers
            .Where(cu => cu.ChatId == chatId)
            .Select(cu => cu.UserId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}