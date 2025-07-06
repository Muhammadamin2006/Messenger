namespace Messenger.Models;

public class OutcomingMessage
{

    public Guid Id { get; set; }

    public string Text { get; set; } = "";
    
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    
    
    public Guid SenderId { get; set; }         
    public User Sender { get; set; }

    
    public Guid? ReceiverUserId { get; set; }      
    public User? ReceiverUser { get; set; }

    
    public Guid? ReceiverGroupId { get; set; }     
    public Group? ReceiverGroup { get; set; }

    
    public List<IncomingMessage> IncomingMessages { get; set; }
}