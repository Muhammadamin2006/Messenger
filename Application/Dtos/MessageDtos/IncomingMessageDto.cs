namespace Messenger.Application.Dtos;

public class IncomingMessageDto
{
    public Guid Id { get; set; }
    public Guid OutgoingMessageId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Text { get; set; } = null!;
    public string SenderName { get; set; } = null!;
    public string ReceiverName { get; set; } = null!;

    public DateTime ReceivedAt { get; set; }
}