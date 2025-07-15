namespace Messenger.Application.Dtos;

public class OutgoingMessageDto
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; } = null!;
    public DateTime SentAt { get; set; }
    public Guid? ReceiverUserId { get; set; }
    public Guid? ReceiverGroupId { get; set; }
}