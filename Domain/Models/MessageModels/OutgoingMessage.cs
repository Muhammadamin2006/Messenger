using Messenger.Domain.Models.UserModels;

namespace Messenger.Domain.Models;

public class OutgoingMessage
{
    public Guid OutgoingMessageId { get; set; } 
    public Guid SenderId { get; set; } 
    public User Sender { get; set; } = null!;
    public string Text { get; set; } = null!;  
    public DateTime OutgoingMessageSentAt { get; set; }  = DateTime.UtcNow;

    
    public Guid? ReceiverUserId { get; set; } 
    public User? ReceiverUser { get; set; }

    public Guid? ReceiverGroupId { get; set; }  
    public Group? ReceiverGroup { get; set; }

    public ICollection<IncomingMessage> IncomingMessages { get; set; } = new List<IncomingMessage>();
}
