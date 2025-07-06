
namespace Messenger.Models;

public class User
{
    public Guid UserId { get; set; } 
    public string Username { get; set; }
    
    public ICollection<IncomingMessage> ReceivedMessages { get; set; }

    public ICollection<OutcomingMessage> SentMessages { get; set; }
    
    public List<UserGroup> Groups { get; set; }
    
}




