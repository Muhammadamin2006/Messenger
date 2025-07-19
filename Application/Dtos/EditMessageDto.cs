namespace Messenger.Application.Dtos;

public class EditMessageDto
{
    public Guid MessageId { get; set; }
    public string NewText { get; set; } = null!;
}