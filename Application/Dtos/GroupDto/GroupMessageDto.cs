namespace Messenger.Application.Dtos.GroupDto;

public class GroupMessageDto
{
    public Guid GroupMessageId { get; set; }
    public Guid GroupId { get; set; }
    
    public Guid SenderId { get; set; }
    public string SenderName { get; set; } = null!;
    
    public DateTime? EditedAt { get; set; }
    public bool IsEdited { get; set; }
    public DateTime? GroupMessageEditedAt { get; set; }
    
    public string Text { get; set; } = null!;
    public DateTime GroupMessageSentAt { get; set; }
}