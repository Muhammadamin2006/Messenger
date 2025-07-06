namespace Messenger.DTOs.Messages;

public class CreateOutcomingMessageDto
{
    public Guid SenderId { get; set; }
    public string Text { get; set; } = "";
    
    public Guid? ReceiverUserId { get; set; } 

    public Guid? ReceiverGroupId { get; set; }
}