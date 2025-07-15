namespace Messenger.Application.Dtos;

public class EditChatMessageDto
{
    public Guid MessageId { get; set; }
    public Guid SenderId { get; set; }
    public string NewText { get; set; } = string.Empty;
}