namespace Messenger.Application.Dtos.GroupDto;

public class SendGroupMessageDto
{
    public Guid GroupId { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; } = null!;
}