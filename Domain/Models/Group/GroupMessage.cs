namespace Messenger.Domain.Models;

public class GroupMessage
{
    public Guid GroupMessageId { get; set; }

    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public Guid SenderId { get; set; }
    public User Sender { get; set; } = null!;

    public string Text { get; set; } = null!;
    public DateTime SentAt { get; set; }
    public bool IsEdited { get; set; }
    public DateTime EditedAt { get; set; }
    public bool IsDeletedForAll { get; set; }

    public ICollection<GroupMessageVisibility> GroupMessageVisibilities { get; set; } = new List<GroupMessageVisibility>();
    
}