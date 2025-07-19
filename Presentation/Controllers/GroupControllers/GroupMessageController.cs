using Messenger.Application.Dtos.GroupDto;
using Messenger.Application.Services.GroupServices;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;

[ApiController]
[Route("api/group-messages")]
public class GroupMessageController : ControllerBase
{
    private readonly IGroupMessageService _groupMessageService;

    public GroupMessageController(IGroupMessageService groupMessageService)
    {
        _groupMessageService = groupMessageService;
    }

    [HttpPost]
    public async Task<ActionResult<GroupMessageDto>> SendMessage([FromBody] CreateGroupMessageDto dto)
    {
        try
        {
            var result = await _groupMessageService.SendMessageAsync(dto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{groupId}")]
    public async Task<ActionResult<List<GroupMessageDto>>> GetVisibleMessages(Guid groupId, [FromQuery] Guid userId)
    {
        var messages = await _groupMessageService.GetMessagesInGroupAsync(groupId, userId);
        return Ok(messages);
    }
    
    
    [HttpPut("edit")]
    public async Task<IActionResult> EditMessage([FromBody] EditGroupMessageDto dto)
    {
        try
        {
            var result = await _groupMessageService.EditMessageAsync(dto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}