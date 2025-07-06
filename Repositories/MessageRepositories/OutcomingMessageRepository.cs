using Messenger.Infrastractures.Database;
using Messenger.Models;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories.MessageRepositories;

public class OutcomingMessageRepository : IOutcomingMessageRepository
{
    private readonly MessengerContext _context;

    public OutcomingMessageRepository(MessengerContext context)
    {
        _context = context;
    }

    public async Task<OutcomingMessage> CreateAsync(OutcomingMessage message)
    {
        _context.OutcomingMessages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<List<Guid>> GetGroupMemberIdsAsync(Guid groupId)
    {
        return await _context.UserGroups
            .Where(ug => ug.GroupId == groupId)
            .Select(ug => ug.UserId)
            .ToListAsync();
    }

}   
    