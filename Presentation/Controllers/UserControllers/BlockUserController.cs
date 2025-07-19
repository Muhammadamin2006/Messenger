using Messenger.Application.Dtos;
using Messenger.Application.Dtos.UserDtos;
using Messenger.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;

public class BlockUserController : ControllerBase
{
    private readonly IBlockUserService _blockUserService;

    public BlockUserController(IBlockUserService blockUserService)
    {
        _blockUserService = blockUserService;
    }

    [HttpPost(Name = "block")]
    public async Task<IActionResult> BlockUser([FromBody] BlockUserDto dto)
    {
        await _blockUserService.BlockUserAsync(dto);
        return Ok("Пользователь успешно заблокирован.");
    }

    [HttpDelete("unblock")]
    public async Task<IActionResult> UnblockUser([FromBody] BlockUserDto dto)
    {
        await _blockUserService.UnblockUserAsync(dto);
        return Ok("Пользователь успешно разблокирован.");
    }

    [HttpGet("status")]
    public async Task<IActionResult> IsBlocked([FromQuery] Guid blockerId, [FromQuery] Guid blockedId)
    {
        var isBlocked = await _blockUserService.IsBlockedAsync(blockerId, blockedId);
        return Ok(isBlocked);
    }

    [HttpGet("block/list/{blockerId}")]
    public async Task<IActionResult> GetBlockedUsers(Guid blockerId)
    {
        var blockedUsers = await _blockUserService.GetBlockedUsersAsync(blockerId);
        return Ok(blockedUsers);
    }

}