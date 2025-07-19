using Messenger.Domain.Models.UserModels;

namespace Messenger.Domain.Models;

public class GroupMessageVisibility
{
    public Guid GroupMessageVisibilityMessageId { get; set; } = Guid.NewGuid();

    public Guid HidByUserId { get; set; }
    public User User { get; set; } = null!;

    public Guid GroupHidMessageId { get; set; }
    public GroupMessage Message { get; set; } = null!;
    public DateTime GroupMessageHiddenAt { get; set; }
}