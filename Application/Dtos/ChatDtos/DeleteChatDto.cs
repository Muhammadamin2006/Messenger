namespace Messenger.Application.Dtos.ChatDtos;

public class DeleteChatDto
{
    public Guid ChatId { get; set; }
    public Guid ChatRemoverId { get; set; }
}