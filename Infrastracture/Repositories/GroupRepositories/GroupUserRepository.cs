using Messenger.Application.Dtos.GroupDto;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories.GroupRepositories;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class GroupUserRepository : GenericRepository<GroupUser>, IGroupUserRepository
{
    private readonly MessengerContext _context;

    public GroupUserRepository(MessengerContext context) : base(context)
    {
        _context = context;
    }
    

    public async Task<bool> IsUserAdminAsync(Guid groupId, Guid userId)
    {
        return await _context.GroupUsers
            .AnyAsync(gu => gu.GroupId == groupId && gu.UserId == userId && gu.IsAdmin);
    }

    public async Task<GroupUser?> GetGroupUserAsync(Guid groupUserId)
    {
        return await _dbSet
            .Include(gu => gu.User)
            .Include(gu => gu.Group)
            .FirstOrDefaultAsync(gu => gu.GroupUserId == groupUserId);
    }

    public async Task<GroupUser?> GetByUserAndGroupAsync(Guid userId, Guid groupId)
    {
        return await _dbSet
            .Include(gu => gu.User)
            .Include(gu => gu.Group)
            .FirstOrDefaultAsync(gu => gu.UserId == userId && gu.GroupId == groupId);
    }

    public async Task<List<GroupUserDto>> GetUsersInGroupChatAsync(Guid groupChatId)
    {
        return await _context.GroupUsers
            .Where(gu => gu.GroupId == groupChatId)
            .Include(gu => gu.User)
            .Select(gu => new GroupUserDto
            {
                GroupUserId = gu.UserId,
                GroupUsername = gu.User.Username,
                Email = gu.User.Email,
                IsAdmin = gu.Status == "Admin"
            })
            .ToListAsync();
    }

    public async Task<bool> IsUserInGroupAsync(Guid groupId, Guid userId)
    {
        return await _context.GroupUsers
            .AnyAsync(gu => gu.GroupId == groupId && gu.UserId == userId);
    }

    public async Task RemoveUserFromGroupChatAsync(Guid userId, Guid groupChatId)
    {
        var groupChatUser = await _context.GroupUsers
            .FirstOrDefaultAsync(gcu => gcu.UserId == userId && gcu.GroupId == groupChatId);

        if (groupChatUser != null)
        {
            _context.GroupUsers.Remove(groupChatUser);
        }
    }

    
    public async Task RemoveAllUsersFromGroupChatAsync(Guid groupChatId)
    {
        var groupUsers = await _context.GroupUsers
            .Where(gcu => gcu.GroupId == groupChatId)
            .ToListAsync();

        _context.GroupUsers.RemoveRange(groupUsers);
    }

    public async Task<List<Guid>> GetUserIdsInGroupChatAsync(Guid groupChatId)
    {
        return await _context.GroupUsers
            .Where(gcu => gcu.GroupId == groupChatId)
            .Select(gcu => gcu.UserId)
            .ToListAsync();
    }
}