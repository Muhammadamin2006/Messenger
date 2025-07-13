namespace Messenger.Domain.Models;

public class UserBlock
{
    public Guid Id { get; set; } = Guid.NewGuid(); 

    public Guid BlockerId { get; set; }             
    public User Blocker { get; set; } = null!;

    public Guid BlockedId { get; set; }            
    public User Blocked { get; set; } = null!;

    public DateTime BlockedAt { get; set; } = DateTime.UtcNow;
}