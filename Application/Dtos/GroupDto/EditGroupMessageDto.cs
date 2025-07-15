namespace Messenger.Application.Dtos.GroupDto;

public class EditGroupMessageDto
{
    public Guid MessageId { get; set; }
    public Guid SenderId { get; set; }
    public string NewText { get; set; } = null!;
}