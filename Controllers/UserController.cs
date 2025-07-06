using Messenger.Api.Repositories;
using Messenger.DTOs;
using Messenger.Infrastractures.Database;
using Messenger.Models;
using Messenger.Repositories;
using Messenger.Services;
using Microsoft.AspNetCore.Mvc;


namespace Messenger.Controllers;
[Route("[controller]")]
[ApiController]


public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost(Name = "CreateUser")]
    public IActionResult CreateUser(CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        var add = _userService.Add(createUserDto);
        if (!add)
            return BadRequest("Failed to add user");
        return Ok("User created successfully");
            
        
    }

    [HttpGet("{id}", Name = "GetUserById")]
    public IActionResult GetUserById(Guid id)
    {
        var user = _userService.GetById(id);
        if (user == null)
            return NotFound("User not found");
        return Created("CreateUser", user);
    }

    [HttpGet(Name = "GetAllUsers")]
    public IActionResult GetAllUsers(UserDto userDto)
    {
        var userr = _userService.GetAll(userDto);
        if (userr == null)
            return NotFound("User not found");
        return Ok(userr);
        
    }

    [HttpPut("{id}", Name = "UpdateUser")]
    public IActionResult UpdateUser(UpdateUserDto updateUserDto)
    {
        var update = _userService.TryUpdate(updateUserDto);
        if (!update)
            return BadRequest("Failed to update user");
        return Ok(update);
    }

    [HttpDelete("{id}", Name = "DeleteUser")]
    public IActionResult DeleteUser(Guid userid)
    {
        var delete =  _userService.TryDelete(userid);
        if (!delete)
            return BadRequest("Failed to delete user");
        return Ok(delete);
    }

    

}



