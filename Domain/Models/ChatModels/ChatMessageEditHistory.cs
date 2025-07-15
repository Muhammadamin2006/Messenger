namespace Messenger.Domain.Models;

public class ChatMessageEditHistory
{
    public Guid EditHistoryId { get; set; }

    public Guid EditMessageId { get; set; }

    public string OldText { get; set; } = string.Empty;

    public DateTime EditedAt { get; set; }
}