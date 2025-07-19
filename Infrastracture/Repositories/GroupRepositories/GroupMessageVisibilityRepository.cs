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
            .AnyAsync(v => v.GroupHidMessageId == messageId && v.HidByUserId == userId);
    }

    public async Task<List<Guid>> GetHiddenMessageIdsAsync(Guid userId, Guid groupChatId)
    {
        return await _context.GroupMessageVisibilities
            .Where(v => v.HidByUserId == userId && v.Message.GroupId == groupChatId)
            .Select(v => v.GroupHidMessageId)
            .ToListAsync();
    }

    
    public async Task HideMessageForUserAsync(Guid messageId, Guid userId)
    {
        var alreadyHidden = await IsMessageHiddenForUserAsync(messageId, userId);
        if (alreadyHidden)
            return;

        var entity = new GroupMessageVisibility
        {
            GroupMessageVisibilityMessageId = Guid.NewGuid(),
            GroupHidMessageId = messageId,
            HidByUserId = userId
        };

        await _context.GroupMessageVisibilities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAllVisibilityRecordsForMessageAsync(Guid messageId)
    {
        var records = await _context.GroupMessageVisibilities
            .Where(v => v.GroupHidMessageId == messageId)
            .ToListAsync();

        _context.GroupMessageVisibilities.RemoveRange(records);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}