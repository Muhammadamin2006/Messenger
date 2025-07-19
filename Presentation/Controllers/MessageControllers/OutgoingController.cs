using Messenger.Application.Dtos;
using Messenger.Application.Dtos.MessageDtos;
using Messenger.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OutgoingController : ControllerBase
{
    private readonly IOutgoingService _outgoingService;

    public OutgoingController(IOutgoingService outgoingService)
    {
        _outgoingService = outgoingService;
    }

    [HttpPost]
    public async Task<IActionResult> Send([FromBody] CreateOutgoingMessageDto dto)
    {
        var result = await _outgoingService.SendMessageAsync(dto);
        return Ok(result);
    }

    [HttpGet("user/{senderId}")]
    public async Task<IActionResult> GetBySender(Guid senderId)
    {
        var messages = await _outgoingService.GetMessagesBySenderIdAsync(senderId);
        return Ok(messages);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _outgoingService.DeleteMessageAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
