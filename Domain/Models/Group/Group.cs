namespace Messenger.Domain.Models;

public class Group
{
    public Guid GroupId { get; set; } = Guid.NewGuid();

    public string GroupName { get; set; } = null!;
    public DateTime GroupCreatedAt { get; set; } = DateTime.UtcNow;

    public Guid GroupCreatedByUserId  { get; set; }
    public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();

    public ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();
    public bool IsAdmin { get; set; }
}