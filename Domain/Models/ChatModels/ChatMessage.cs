using Messenger.Domain.Models.UserModels;

namespace Messenger.Domain.Models;

public class ChatMessage
{
    public Guid MessageId { get; set; } = Guid.NewGuid();

    public Guid ChatId { get; set; }
    public Chat Chat { get; set; } = null!;

    public Guid SenderId { get; set; }
    public User Sender { get; set; } = null!;

    public string Text { get; set; } = null!;

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public DateTime? EditedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    
    public ICollection<ChatMessageVisibility> Visibilities { get; set; } = new List<ChatMessageVisibility>();
}