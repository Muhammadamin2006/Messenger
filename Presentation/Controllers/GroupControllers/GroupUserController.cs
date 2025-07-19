using Messenger.Application.Dtos.GroupDto;
using Messenger.Application.Services.GroupServices;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;

[ApiController]
[Route("api/group-users")]
public class GroupUserController : ControllerBase
{
    private readonly IGroupUserService _groupUserService;

    public GroupUserController(IGroupUserService groupUserService)
    {
        _groupUserService = groupUserService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserToGroup([FromBody] AddGroupUserDto dto)
    {
        var result = await _groupUserService.AddUserToGroupAsync(dto);
        if (!result)
            return BadRequest("Пользователь уже состоит в группе.");

        return Ok("Пользователь успешно добавлен в группу.");
    }
}