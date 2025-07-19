using Messenger.Domain.Models.UserModels;

namespace Messenger.Domain.Models;

public class GroupUser
{
    public Guid GroupUserId { get; set; } = Guid.NewGuid();

    public string GroupUsername { get; set; } =  null!;
    
    public Guid GroupId { get; set; }

    public Group Group { get; set; } = null!;

    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;
    
    public bool IsAdmin { get; set; }
    public string Status { get; set; } = "Member";


    public DateTime UserJoinedToThisGroupAt { get; set; } = DateTime.UtcNow;
}