using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class GroupUserRepository : GenericRepository<GroupUser>, IGroupUserRepository
{
    private readonly MessengerContext _context;

    public GroupUserRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsUserInGroupAsync(Guid userId, Guid groupId)
    {
        return await _context.GroupUsers
            .AnyAsync(x => x.UserId == userId && x.GroupId == groupId);
    }

    public async Task<List<GroupUser>> GetGroupUsersAsync(Guid groupId)
    {
        return await _context.GroupUsers
            .Where(x => x.GroupId == groupId)
            .Include(x => x.User)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
