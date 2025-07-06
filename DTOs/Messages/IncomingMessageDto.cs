namespace Messenger.DTOs.Messages;

public class IncomingMessageDto
{
    public Guid Id { get; set; }
    public Guid OutgoingMessageId { get; set; }
    public OutcomingMessageDto OutcomingMessage { get; set; } = null!;

    public Guid ReceiverId { get; set; }
    public string ReceiverUsername { get; set; } = "";

    public bool IsRead { get; set; }
}