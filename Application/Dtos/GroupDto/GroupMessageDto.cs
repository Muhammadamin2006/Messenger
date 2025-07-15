namespace Messenger.Application.Dtos.GroupDto;

public class GroupMessageDto
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public Guid SenderId { get; set; }
    public string SenderName { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime SentAt { get; set; }
    public DateTime? EditedAt { get; set; }
}