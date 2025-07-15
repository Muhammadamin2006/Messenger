using Messenger.Application.Services;
using Messenger.Infrastracture.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;

[ApiController]
[Route("ChatMessage")]

public class ChatMessageVisibilityController : ControllerBase
{
    private readonly IChatMessageVisibilityService _chatMessageVisibilityService;

    public ChatMessageVisibilityController(IChatMessageVisibilityService chatMessageVisibilityService)
    {
        _chatMessageVisibilityService = chatMessageVisibilityService;
    }
    
    [HttpDelete("forme/{messageId}")]
    public async Task<IActionResult> DeleteForMe(Guid messageId, [FromQuery] Guid userId)
    {
        await _chatMessageVisibilityService.DeleteMessageForMeAsync(messageId, userId);
        return NoContent();
    }

    [HttpDelete("forall/{messageId}")]
    public async Task<IActionResult> DeleteForAll(Guid messageId, [FromQuery] Guid userId)
    {
        await _chatMessageVisibilityService.DeleteMessageForEveryoneAsync(messageId, userId);
        return NoContent();
    }
}