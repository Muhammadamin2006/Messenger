namespace Messenger.Domain.Models;

public class ChatMessageVisibility
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid MessageId { get; set; }

    public ChatMessage Message { get; set; } = null!;
}