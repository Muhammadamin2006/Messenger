using Messenger.Application.Dtos;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class ChatRepository : GenericRepository<Chat>, IChatRepository
{
    private readonly MessengerContext _context;

    public ChatRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Chat?> GetChatBetweenUsersAsync(Guid firstUserId, Guid secondUserId)
    {
        return await _context.Chats
            .FirstOrDefaultAsync(c =>
                (c.FirstUserId == firstUserId && c.SecondUserId == secondUserId) ||
                (c.FirstUserId == secondUserId && c.SecondUserId == firstUserId));
    }

    public async Task<List<Chat>> GetChatsForUserAsync(Guid userId)
    {
        return await _context.Chats
            .Where(c => c.FirstUserId == userId || c.SecondUserId == userId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}