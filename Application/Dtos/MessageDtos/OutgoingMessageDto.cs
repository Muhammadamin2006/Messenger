namespace Messenger.Application.Dtos.MessageDtos;

public class OutgoingMessageDto
{
    public Guid OutgoingMessageId { get; set; }             
    public Guid SenderId { get; set; }       
    public string Text { get; set; } = null!; 
    public DateTime OutgoingMessageSentAt { get; set; }      

    public Guid? ReceiverUserId { get; set; }    
    public Guid? ReceiverGroupId { get; set; }
}