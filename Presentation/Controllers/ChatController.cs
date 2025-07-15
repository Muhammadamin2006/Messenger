using Messenger.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("between-create")]
    public async Task<IActionResult> CreateBetweenUsersChat([FromQuery] Guid firstUserId, [FromQuery] Guid secondUserId)
    {
        var chat = await _chatService.CreateBetweenUsersChatAsync(firstUserId, secondUserId);
        return Ok(chat);
    }

    [HttpGet("between-get")]
    public async Task<IActionResult> GetBetweenUsersChat([FromQuery] Guid firstUserId, [FromQuery] Guid secondUserId)
    {
        var chat = await _chatService.GetBetweenUsersChatAsync(firstUserId, secondUserId);
        return chat == null ? NotFound("Чат не найден") : Ok(chat);
    }

    [HttpGet("get-user-all-chat/{userId}")]
    public async Task<IActionResult> GetChatsForUser(Guid userId)
    {
        var chats = await _chatService.GetChatsForUserAsync(userId);
        return Ok(chats);
    }
}