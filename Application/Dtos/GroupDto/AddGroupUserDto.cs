namespace Messenger.Application.Dtos.GroupDto;

public class AddGroupUserDto
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
}