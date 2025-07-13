namespace Messenger.Domain.Models;

public class OutgoingMessage
{
    public Guid Id { get; set; } 
    public Guid SenderId { get; set; } 
    public User Sender { get; set; } = null!;
    public string Text { get; set; } = null!;  
    public DateTime SentAt { get; set; }  

    
    public Guid? ReceiverUserId { get; set; } 
    public User? ReceiverUser { get; set; }

    public Guid? ReceiverGroupId { get; set; }  
    public Group? ReceiverGroup { get; set; }

    public ICollection<IncomingMessage> IncomingMessages { get; set; } = new List<IncomingMessage>();
}
