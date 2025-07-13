namespace Messenger.Application.Dtos;

public class CreateOutgoingMessageDto
{
    public Guid SenderId { get; set; }

    public string Text { get; set; } = null!;

    public Guid? ReceiverUserId { get; set; }

    public Guid? ReceiverGroupId { get; set; }
}