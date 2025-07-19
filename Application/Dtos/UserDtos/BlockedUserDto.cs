namespace Messenger.Application.Dtos.UserDtos;

public class BlockedUserDto
{
    public Guid BlockedUserId { get; set; }   
    
    public string BlockedUsername { get; set; } = null!;
    
    public DateTime BlockedAt { get; set; }
    
}