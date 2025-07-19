using Messenger.Domain.Models.UserModels;

namespace Messenger.Domain.Models;

public class Chat
{
    public Guid ChatId { get; set; } = Guid.NewGuid(); 

    public DateTime ChatCreatedAt { get; set; } = DateTime.UtcNow;

    public Guid FirstUserId { get; set; }
    public User FirstUser { get; set; } = null!;

    public Guid SecondUserId { get; set; }
    public User SecondUser { get; set; } = null!;


    public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();

    public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    
}