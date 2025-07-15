namespace Messenger.Domain.Models;

public class EditGroupMessage
{
    public Guid Id { get; set; }

    public Guid MessageId { get; set; }
    public GroupMessage Message { get; set; } = null!;

    public string OldText { get; set; } = null!;
    public DateTime EditedAt { get; set; }
}