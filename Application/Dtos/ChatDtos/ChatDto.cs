namespace Messenger.Application.Dtos;

public class ChatDto
{
    public Guid Id { get; set; }
    public Guid FirstUserId { get; set; }
    public string FirstUserName { get; set; } = null!;
    public Guid SecondUserId { get; set; }
    public string SecondUserName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}