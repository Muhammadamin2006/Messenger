namespace Messenger.Domain.Models;

public class Group
{
    public Guid GroupId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
    public ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();
}