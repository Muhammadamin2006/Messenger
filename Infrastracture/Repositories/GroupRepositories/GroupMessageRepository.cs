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

    public async Task<List<GroupMessage>> GetMessagesByGroupChatIdAsync(Guid groupChatId)
    {
        return await _context.GroupMessages
            .Where(m => m.GroupId == groupChatId)
            .Include(m => m.GroupSender)
            .OrderBy(m => m.GroupMessageSentAt)
            .ToListAsync();
    }

    public async Task<GroupMessage?> GetMessageByIdAsync(Guid messageId)
    {
        return await _context.GroupMessages
            .Include(m => m.GroupSender)
            .FirstOrDefaultAsync(m => m.GroupMessageId == messageId);
    }

    public async Task EditMessageAsync(Guid messageId, Guid userId, string newText)
    {
        var message = await _context.GroupMessages.FirstOrDefaultAsync(m => m.GroupMessageId == messageId);

        if (message == null)
            throw new Exception("Message not found");

        if (message.GroupSenderId != userId)
            throw new Exception("You can edit only your messages");

        message.Text = newText;
        message.GroupMessageEditedAt = DateTime.UtcNow;

        _context.GroupMessages.Update(message);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMessagesByGroupIdAsync(Guid groupChatId)
    {
        var messages = await _context.GroupMessages
            .Where(m => m.GroupId == groupChatId)
            .ToListAsync();

        _context.GroupMessages.RemoveRange(messages);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}