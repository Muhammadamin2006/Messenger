using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Application.Dtos.UserDtos;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Application.Services;

public class BlockUserService : IBlockUserService
{
    private readonly IUserBlocksRepository _userBlocksRepository;
    private readonly MessengerContext _context;
    private readonly IMapper _mapper;

    public BlockUserService(IUserBlocksRepository userBlocksRepository, MessengerContext context, IMapper mapper)
    {
        _userBlocksRepository = userBlocksRepository;
        _context = context;
        _mapper = mapper;
    }

    public async Task BlockUserAsync(BlockUserDto dto)
    {
        await _userBlocksRepository.BlockUserAsync(dto.BlockerId, dto.BlockedUserId);
        await _context.SaveChangesAsync();
    }

    public async Task UnblockUserAsync(BlockUserDto dto)
    {
        await _userBlocksRepository.UnblockUserAsync(dto.BlockerId, dto.BlockedUserId);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsBlockedAsync(Guid blockerId, Guid blockedId)
    {
        return await _userBlocksRepository.IsBlockedAsync(blockerId, blockedId);
    }

    public async Task<List<BlockedUserDto>> GetBlockedUsersAsync(Guid blockerId)
    {
        var blockedUsers = await _context.UserBlocks
            .Where(b => b.BlockerId == blockerId)
            .Include(b => b.Blocked)
            .ToListAsync();

        // Преобразуем в DTO
        var result = blockedUsers.Select(b => new BlockedUserDto
        {
            BlockedUserId = b.Blocked.UserId,
            BlockedUsername = b.Blocked.Username,
            BlockedAt = b.BlockedAt
        }).ToList();

        return result;
    }
}