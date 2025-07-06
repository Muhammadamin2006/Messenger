using Messenger.DTOs.Messages;

namespace Messenger.Models;

public class IncomingMessage()
{
    public Guid Id { get; set; }

    public Guid OutcomingMessageId { get; set; }  
    public OutcomingMessage OutcomingMessage { get; set; }

    public Guid ReceiverId { get; set; }         
    public User Receiver { get; set; }

    public bool IsRead { get; set; } = false;
    
}