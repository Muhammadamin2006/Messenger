namespace Messenger.Application.Dtos.ChatDtos;

public class ChatDto
{
    public Guid ChatId { get; set; }
    
    public Guid FirstUserId { get; set; }
    public string FirstUserName { get; set; } = null!;
    
    public Guid SecondUserId { get; set; }
    public string SecondUserName { get; set; } = null!;
    
    public DateTime ChatCreatedAt { get; set; }
}