namespace Messenger.DTOs;

public class UserDto
{
    public Guid UserId { get; set; } =  Guid.NewGuid();
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}