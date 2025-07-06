namespace Messenger.DTOs;

public class CreateUserDto
{
    public Guid UserId { get; set; } =  Guid.NewGuid();
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}