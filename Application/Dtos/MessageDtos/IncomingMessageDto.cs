namespace Messenger.Application.Dtos.MessageDtos;

public class IncomingMessageDto
{
    public Guid IncomingMessageId { get; set; }           
    
    public string SenderName { get; set; } = null!;   
    public Guid OutgoingMessageId { get; set; }     
    
    public string ReceiverName { get; set; } = null!;  
    public Guid ReceiverId { get; set; }      
    
    public string Text { get; set; } = null!;          

    public DateTime ReceivedAt { get; set; }          
    public DateTime? ReadAt { get; set; }
    
    public bool IsRead { get; set; }                  
}