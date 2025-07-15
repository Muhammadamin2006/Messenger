using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class GroupMessageRepository : GenericRepository<GroupMessage>, IGroupMessageRepository
{
    private readonly MessengerContext _context;

    public GroupMessageRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<GroupMessage>> GetMessagesByGroupIdAsync(Guid groupId)
    {
        return await _context.GroupMessages
            .Where(m => m.GroupId == groupId && !m.IsDeletedForAll)
            .Include(m => m.Sender)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task<GroupMessage?> GetByIdWithSenderAsync(Guid messageId)
    {
        return await _context.GroupMessages
            .Include(m => m.Sender)
            .FirstOrDefaultAsync(m => m.GroupMessageId == messageId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}