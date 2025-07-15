namespace Messenger.Domain.Models;

public class Chat
{
    public Guid ChatId { get; set; }

    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public User FirstUser { get; set; } = null!;
    public User SecondUser { get; set; } = null!;

    public ICollection<OutgoingMessage> Messages { get; set; } = new List<OutgoingMessage>();
    public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    
}