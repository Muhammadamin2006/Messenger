namespace Messenger.Application.Dtos;

public class CreateChatMessageDto
{
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; } = string.Empty;
}