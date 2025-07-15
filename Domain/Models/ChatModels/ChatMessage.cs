namespace Messenger.Domain.Models;

public class ChatMessage
{
    public Guid MessageId { get; set; }      
    public Guid ChatId { get; set; }                  
    public Guid ReceivedChatId { get; set; }     
    public Guid SenderId { get; set; }    
    public string Text { get; set; } = null!;  
    public DateTime SentAt { get; set; }  
    public DateTime? EditedAt { get; set; }
    public Chat Chat { get; set; } = null!;     
    public User Sender { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
}