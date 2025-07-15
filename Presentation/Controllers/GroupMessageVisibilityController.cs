using Messenger.Application.Services.GroupServices;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;

[ApiController]
[Route("api/group-messages")]
public class GroupMessageVisibilityController : ControllerBase
{
    private readonly IGroupMessageVisibilityService _visibilityService;

    public GroupMessageVisibilityController(IGroupMessageVisibilityService visibilityService)
    {
        _visibilityService = visibilityService;
    }

    [HttpDelete("forme/{messageId}")]
    public async Task<IActionResult> DeleteForMe(Guid messageId, [FromQuery] Guid userId)
    {
        var result = await _visibilityService.DeleteMessageForMeAsync(messageId, userId);
        if (!result) return NotFound("Сообщение уже скрыто или не найдено");
        return NoContent();
    }

    [HttpDelete("forall/{messageId}")]
    public async Task<IActionResult> DeleteForAll(Guid messageId, [FromQuery] Guid userId)
    {
        var result = await _visibilityService.DeleteMessageForEveryoneAsync(messageId, userId);
        if (!result) return Forbid("Ты не автор сообщения или сообщение не найдено");
        return NoContent();
    }
}