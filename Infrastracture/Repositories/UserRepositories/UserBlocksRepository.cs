using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class UserBlocksRepository : GenericRepository<User>, IUserBlocksRepository
{
    private readonly DbSet<UserBlock> _userBlocks;

    public UserBlocksRepository(MessengerContext context) : base(context)
    {
        _userBlocks = context.Set<UserBlock>();
    }


    public async Task BlockUserAsync(Guid blockerId, Guid blockedId)
    {
        if (blockerId == blockedId)
            return;

        var exists = await _userBlocks.AnyAsync(b =>
            b.BlockerId == blockerId && b.BlockedId == blockedId);

        if (!exists)
        {
            await _userBlocks.AddAsync(new UserBlock
            {
                Id = Guid.NewGuid(),
                BlockerId = blockerId,
                BlockedId = blockedId,
                BlockedAt = DateTime.UtcNow 
            });

        }
    }

    public async Task UnblockUserAsync(Guid blockerId, Guid blockedId)
    {
        var unblock = await _userBlocks
            .FirstOrDefaultAsync(b =>
                b.BlockerId == blockerId && b.BlockedId == blockedId);

        if (unblock != null)
        {
            _userBlocks.Remove(unblock);
        }
        

    }

    public async Task<bool> IsBlockedAsync(Guid blockerId, Guid blockedId)
    {
        return await _userBlocks.AnyAsync(b =>
            b.BlockerId == blockerId && b.BlockedId == blockedId);
        
    }

    public async Task<List<User>> GetBlockedUsersAsync(Guid blockerId)
    {
        return await _userBlocks
            .Where(b => b.BlockerId == blockerId)
            .Include(b => b.Blocked)
            .Select(b => b.Blocked)
            .ToListAsync();
    }
}