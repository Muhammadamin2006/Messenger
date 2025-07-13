namespace Messenger.Application.Dtos;

public class BlockedUserDto
{
    public Guid Id { get; set; }   
    public string Username { get; set; } = "";
    public DateTime BlockedAt { get; set; }
}