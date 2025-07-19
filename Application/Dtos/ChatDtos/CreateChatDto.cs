namespace Messenger.Application.Dtos.ChatDtos;

public class CreateChatDto
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
}