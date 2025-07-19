namespace Messenger.Application.Dtos.UserDtos;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime UserCreatedAt { get; set; }
    
}