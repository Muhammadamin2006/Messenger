using Messenger.Application.Dtos;
using Messenger.Application.Dtos.UserDtos;

namespace Messenger.Application.Services;

public interface IBlockUserService
{
    Task BlockUserAsync(BlockUserDto dto);

    Task UnblockUserAsync(BlockUserDto dto);

    Task<bool> IsBlockedAsync(Guid blockerId, Guid blockedId);

    Task<List<BlockedUserDto>> GetBlockedUsersAsync(Guid blockerId);
}