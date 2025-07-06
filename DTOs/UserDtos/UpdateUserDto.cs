namespace Messenger.DTOs;

public class UpdateUserDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    
}