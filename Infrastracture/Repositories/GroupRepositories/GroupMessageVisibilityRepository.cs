using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class GroupMessageVisibilityRepository : GenericRepository<GroupMessageVisibility>, IGroupMessageVisibilityRepository
{
    private readonly MessengerContext _context;

    public GroupMessageVisibilityRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsMessageHiddenForUserAsync(Guid messageId, Guid userId)
    {
        return await _context.GroupMessageVisibilities
            .AnyAsync(x => x.MessageId == messageId && x.UserId == userId);
    }

    public async Task<List<Guid>> GetHiddenMessageIdsForUserAsync(Guid userId)
    {
        return await _context.GroupMessageVisibilities
            .Where(v => v.UserId == userId)
            .Select(v => v.MessageId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}