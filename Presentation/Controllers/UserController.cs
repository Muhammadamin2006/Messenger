using Messenger.Application.Dtos;
using Messenger.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace Messenger.Presentation.Controllers;

[ApiController]
[Route("api/controller")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
   
    
    [HttpGet("all")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
}