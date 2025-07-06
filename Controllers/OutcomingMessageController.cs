using Messenger.DTOs.Messages;
using Messenger.Services.MessageServices;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;
[ApiController]
[Route("api/[controller]")]

public class OutcomingMessageController : ControllerBase
{
    private readonly IOutcomingMessageService _outcomingMessageService;

    public OutcomingMessageController(IOutcomingMessageService outcomingMessageService)
    {
        _outcomingMessageService = outcomingMessageService;
    }

    [HttpPost]
    public async Task<ActionResult<OutcomingMessageDto>> Post([FromBody] CreateOutcomingMessageDto createOutcomingMessageDto)
    {
        if (createOutcomingMessageDto == null)
            return BadRequest();
        
        var create = await _outcomingMessageService.CreateAsync(createOutcomingMessageDto);
        return CreatedAtAction(nameof(Post), new { id = create.Id }, create);
    }
    


   
    
}
