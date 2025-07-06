namespace Messenger.DTOs.Messages;

public class OutcomingMessageDto
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public string SenderUsername { get; set; } = "";

    public string Text { get; set; } = "";
    public DateTime SentAt { get; set; }

    public Guid? ReceiverUserId { get; set; }
    public string? ReceiverUsername { get; set; }

    public Guid? ReceiverGroupId { get; set; }
    public string? ReceiverGroupName { get; set; }
}