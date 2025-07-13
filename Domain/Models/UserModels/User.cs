namespace Messenger.Domain.Models;

public class User
{
    public Guid Id { get; set; } =  Guid.NewGuid();

    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    
    public ICollection<UserBlock> BlockedUsers { get; set; } = new List<UserBlock>();
    public ICollection<UserBlock> BlockedByUsers { get; set; } = new List<UserBlock>();

    public ICollection<OutgoingMessage> SentMessages { get; set; } = new List<OutgoingMessage>();

    public ICollection<IncomingMessage> ReceivedMessages { get; set; } = new List<IncomingMessage>();

    public ICollection<GroupUser> UserGroups { get; set; } = new List<GroupUser>();
}