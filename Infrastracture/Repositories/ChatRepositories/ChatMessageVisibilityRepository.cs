using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class ChatMessageVisibilityRepository : GenericRepository<ChatMessageVisibility>, IChatMessageVisibilityRepository
{
    private readonly MessengerContext _context;

    public ChatMessageVisibilityRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsMessageHiddenForUserAsync(Guid messageId, Guid userId)
    {
        return await _context.ChatMessageVisibilities
            .AnyAsync(v => v.MessageId == messageId && v.UserId == userId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
