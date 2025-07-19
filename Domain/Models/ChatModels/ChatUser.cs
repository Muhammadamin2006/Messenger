using Messenger.Domain.Models.UserModels;

namespace Messenger.Domain.Models;

public class ChatUser
{
    public Guid ChatUserId { get; set; }
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime UserJoinedToChatAt { get; set; }
    
    public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
    public string Status { get; set; } = "Member";
}