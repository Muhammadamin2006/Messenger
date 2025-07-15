namespace Messenger.Domain.Models;

public class GroupUser
{
    public Guid GroupUserId { get; set; }
    
    public Guid GroupId { get; set; }          
    public Group Group { get; set; } = null!;

    public Guid UserId { get; set; }          
    public User User { get; set; } = null!;

    public DateTime JoinedAt { get; set; }

    public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
    public ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();
}