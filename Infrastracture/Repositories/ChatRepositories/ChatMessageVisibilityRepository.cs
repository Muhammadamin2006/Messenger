using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class ChatMessageVisibilityRepository : GenericRepository<ChatMessageVisibility>, IChatMessageVisibilityRepository
{
    private readonly MessengerContext _context;
    private readonly DbSet<ChatMessageVisibility> _chatMessageVisibility;

    public ChatMessageVisibilityRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsMessageHiddenForUserAsync(Guid messageId, Guid userId)
    {
        return await _context.ChatMessageVisibilities
            .AnyAsync(v => v.HiddenMessageId == messageId && v.HidUserId == userId);
    }
    
    public async Task<List<Guid>> GetHiddenMessageIdsAsync(Guid userId, Guid chatId)
    {
        return await _chatMessageVisibility
            .Where(v => v.HidUserId == userId && v.Message.ChatId == chatId)
            .Select(v => v.HiddenMessageId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
