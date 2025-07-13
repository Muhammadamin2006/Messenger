using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;

namespace Messenger.Application.Services;

public class BlockUserService  : IBlockUserService
{
    private readonly IUserBlocksRepository  _userBlocksRepository;
    private readonly MessengerContext _context;

    public BlockUserService(IUserBlocksRepository userBlocksRepository,
        MessengerContext context)
    {
        _userBlocksRepository = userBlocksRepository;
        _context = context;
    }
    
    public async Task BlockUserAsync(BlockUserDto dto)
    {
        await _userBlocksRepository.BlockUserAsync(dto.BlockerId, dto.BlockedId);
        await _context.SaveChangesAsync();
    }

    public async Task UnblockUserAsync(BlockUserDto dto)
    {
        await _userBlocksRepository.UnblockUserAsync(dto.BlockerId, dto.BlockedId);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsBlockedAsync(Guid blockerId, Guid blockedId)
    {
        return await _userBlocksRepository.IsBlockedAsync(blockerId, blockedId);
    }

    public async Task<List<BlockedUserDto>> GetBlockedUsersAsync(Guid blockerId)
    {
        var blockedUsers = await _userBlocksRepository.GetBlockedUsersAsync(blockerId);

        var result = blockedUsers.Select(user => 
            new BlockedUserDto 
            {    
                
                Id = user.Id, Username = user.Username, BlockedAt = DateTime.UtcNow 
            
            }).ToList();

        return result;
    }
}