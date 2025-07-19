using Messenger.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class IncomingController : ControllerBase
{
    private readonly IIncomingService _incomingService;

    public IncomingController(IIncomingService incomingService)
    {
        _incomingService = incomingService;
    }

    [HttpGet("user/{receiverId}")]
    public async Task<IActionResult> GetByReceiver(Guid receiverId)
    {
        var messages = await _incomingService.GetMessagesByReceiverIdAsync(receiverId);
        return Ok(messages);
    }


    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        try
        {
            await _incomingService.MarkAsReadAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
