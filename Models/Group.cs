namespace Messenger.Models;

public class Group
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = "";
    
    public List<UserGroup> Members { get; set; }
}