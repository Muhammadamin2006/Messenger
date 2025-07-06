using Messenger.DTOs.Messages;
using Messenger.Infrastractures.Database;
using Messenger.Repositories.MessageRepositories;
using Messenger.Services.MessageServices;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Route("api/[controller]")]


public class IncomingMessageController : ControllerBase
{
    private readonly IIncomingMessageService _incomingMessageService;

    public IncomingMessageController(IIncomingMessageService incomingMessageService)
    {
        _incomingMessageService = incomingMessageService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncomingMessageDto>> GetById(Guid id)
    { 
        var message = await _incomingMessageService.GetByIdAsync(id);
        if (message == null)
            return NotFound();
        return Ok(message);
     
        
    }
}    
    
    




