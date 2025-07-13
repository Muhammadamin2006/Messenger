namespace Messenger.Domain.Models;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
    
    public ICollection<OutgoingMessage> Messages { get; set; } = new List<OutgoingMessage>();
}