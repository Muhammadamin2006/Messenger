using Messenger.Domain.Models.UserModels;

namespace Messenger.Domain.Models;

public class GroupMessage
{
    public Guid GroupMessageId { get; set; } = Guid.NewGuid(); 

    public Guid GroupId { get; set; }                          
    public Group Group { get; set; } = null!;                  

    public Guid GroupSenderId { get; set; }                         
    public User GroupSender { get; set; } = null!;                  
    
    public string Text { get; set; } = null!;                 

    public DateTime GroupMessageSentAt { get; set; } = DateTime.UtcNow;    
    
    public bool IsEdited { get; set; } = false;
    public DateTime? GroupMessageEditedAt { get; set; }           
    
    public bool IsGroupMessageEdited { get; set; } = false;            
    
    public bool IsGroupMessageDeletedForAll { get; set; } = false;         

    public ICollection<GroupMessageVisibility> HiddenByUsers { get; set; } = new List<GroupMessageVisibility>();
    
}