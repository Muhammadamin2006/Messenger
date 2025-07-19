using Messenger.Domain.Models.UserModels;

namespace Messenger.Domain.Models;

public class IncomingMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OutgoingMessageId { get; set; }
    public OutgoingMessage OutgoingMessage { get; set; } = null!;

    public Guid ReceiverId { get; set; }
    public User Receiver { get; set; } = null!;

    public bool IsRead { get; set; } = false;

    public DateTime ReceivedAt { get; set; }

    public DateTime? ReadAt { get; set; }

}
