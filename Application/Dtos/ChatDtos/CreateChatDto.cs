namespace Messenger.Application.Dtos;

public class CreateChatDto
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
}