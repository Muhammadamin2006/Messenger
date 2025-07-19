namespace Messenger.Application.Dtos.GroupDto;

public class RemoveGroupUserByAdminDto
{
    public Guid GroupId { get; set; }      
    public Guid UserId { get; set; } 
    public Guid AdminId { get; set; }
}