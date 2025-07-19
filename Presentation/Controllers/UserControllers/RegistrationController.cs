using Messenger.Application.Dtos;
using Messenger.Application.Dtos.UserDtos;
using Messenger.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IUserService _userService;

    public RegistrationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Registration")]
    public async Task<ActionResult<UserDto>> RegisterUserAsync([FromBody] RegistrationDto registrationDto)
    {
        var user = await _userService.RegisterUserAsync(registrationDto);
        return Ok(user);

        
    }
    
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        try
        {
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}