using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class GroupRepository : GenericRepository<Group>, IGroupRepository
{
    private readonly DbSet<Group> _groups;

    public GroupRepository(MessengerContext context) : base(context)
    {
        _groups = context.Set<Group>();
    }

    public async Task<Group?> GetByIdWithUsersAsync(Guid groupId)
    {
        return await _groups
            .Include(g => g.GroupUsers)
            .ThenInclude(ug => ug.User)
            .FirstOrDefaultAsync(g => g.Id == groupId);
    }
}