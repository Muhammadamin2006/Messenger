namespace Messenger.Application.Dtos.GroupDto;

public class CreateGroupMessageDto
{
    public Guid GroupId { get; set; }
    public Guid GroupSenderId { get; set; }
    public string Text { get; set; } = null!;
}