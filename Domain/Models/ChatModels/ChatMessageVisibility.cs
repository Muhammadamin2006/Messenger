using Messenger.Domain.Models.UserModels;

namespace Messenger.Domain.Models;

public class ChatMessageVisibility
{
    public Guid ChatMessageVisibilityId { get; set; }

    public Guid HidUserId { get; set; }
    public User User { get; set; } = null!;
    
    public Guid HiddenMessageId { get; set; }
    public ChatMessage Message { get; set; } = null!;
}