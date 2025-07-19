using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories.GroupRepositories;

public class GroupRepository : GenericRepository<Group>, IGroupRepository
{
    private readonly MessengerContext _context;

    public GroupRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Group?> GetGroupChatByIdAsync(Guid groupChatId)
    {
        return await _context.Groups
            .Include(gc => gc.GroupUsers)
            .Include(gc => gc.GroupMessages)
            .FirstOrDefaultAsync(gc => gc.GroupId == groupChatId);
    }

    public async Task<List<Group>> GetGroupChatsForUserAsync(Guid userId)
    {
        return await _context.Groups
            .Include(gc => gc.GroupUsers)
            .Where(gc => gc.GroupUsers.Any(u => u.UserId == userId))
            .ToListAsync();
    }

    public async Task<bool> GroupChatExistsAsync(Guid groupChatId)
    {
        return await _context.Groups.AnyAsync(gc => gc.GroupId == groupChatId);
    }

    public async Task DeleteGroupChatCompletelyAsync(Guid groupChatId)
    {
        var groupChat = await _context.Groups
            .Include(gc => gc.GroupUsers)
            .Include(gc => gc.GroupMessages)
                .ThenInclude(m => m.HiddenByUsers)
            .FirstOrDefaultAsync(gc => gc.GroupId == groupChatId);

        if (groupChat == null)
            return;

        foreach (var message in groupChat.GroupMessages)
        {
            _context.GroupMessageVisibilities.RemoveRange(message.HiddenByUsers);
        }
        
        _context.GroupMessages.RemoveRange(groupChat.GroupMessages);
        _context.GroupUsers.RemoveRange(groupChat.GroupUsers);
        _context.Groups.Remove(groupChat);

        await _context.SaveChangesAsync();
        
    }

    public async Task<Group?> GetByNameAndUserEmailAsync(string groupName, string userEmail)
    {
        return await _dbSet
            .Include(g => g.GroupUsers)             
            .ThenInclude(m => m.User)            
            .FirstOrDefaultAsync(g =>
                g.GroupName.ToLower() == groupName.ToLower() &&
                g.GroupUsers.Any(m => m.User.Email == userEmail));
    }

    public async Task<bool> IsGroupNameTakenByUserAsync(string groupName, Guid userId)
    {
        return await _dbSet.AnyAsync(g =>
            g.GroupName.ToLower() == groupName.ToLower() &&
            g.GroupCreatedByUserId == userId);
    }
    


    public async Task<Group?> GetGroupWithUsersAsync(Guid groupId)
    {
        return await _dbSet
            .Include(g => g.GroupUsers)
            .ThenInclude(gu => gu.User)
            .FirstOrDefaultAsync(g => g.GroupId == groupId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsGroupNameTakenForUserAsync(string groupName, string userEmail)
    {
        return await _context.Groups
            // .Include(g => g.GroupUsers)
            .AnyAsync(g => g.GroupName == groupName && g.GroupUsers.Any(gu => gu.User.Email == userEmail));
    }
}
