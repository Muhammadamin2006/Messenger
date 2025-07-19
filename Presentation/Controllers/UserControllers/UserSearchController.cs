using Messenger.Application.Dtos;
using Messenger.Application.Dtos.UserDtos;
using Messenger.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;

public class UserSearchController : ControllerBase
{
    private readonly IUserService _userService;

    public UserSearchController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("by-phone")]
    public async Task<ActionResult<UserDto>> GetUserByPhone([FromQuery] string phoneNumber)
    {
        var user = await _userService.FindUserByPhoneNumberAsync(phoneNumber);
        if (user == null)
            return NotFound(new { message = "Пользователь не найден" });

        return Ok(user);
    }
}