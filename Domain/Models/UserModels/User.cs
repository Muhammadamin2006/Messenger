namespace Messenger.Domain.Models.UserModels;

public class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();

    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime UserCreatedAt { get; set; }


    public ICollection<OutgoingMessage> SentMessages { get; set; } = new List<OutgoingMessage>();
    public ICollection<IncomingMessage> ReceivedMessages { get; set; } = new List<IncomingMessage>();


    public ICollection<UserBlock> BlockedByMe { get; set; } = new List<UserBlock>();         
    public ICollection<UserBlock> BlockedByUsers { get; set; } = new List<UserBlock>();      


    public ICollection<Chat> ChatsCreator { get; set; } = new List<Chat>();     
    public ICollection<Chat> ChatsReceived { get; set; } = new List<Chat>();      

    
    public ICollection<ChatMessage> SentChatMessages { get; set; } = new List<ChatMessage>(); 
    public ICollection<ChatMessageVisibility> HiddenChatMessages { get; set; } = new List<ChatMessageVisibility>(); 


    public ICollection<GroupUser> UserGroups { get; set; } = new List<GroupUser>(); 
    public ICollection<GroupMessage> SentGroupMessages { get; set; } = new List<GroupMessage>(); 
    public ICollection<GroupMessageVisibility> HiddenGroupMessages { get; set; } = new List<GroupMessageVisibility>();
}