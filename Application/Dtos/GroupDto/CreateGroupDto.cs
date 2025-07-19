namespace Messenger.Application.Dtos.GroupDto;

public class CreateGroupDto
{
    public string GroupName { get; set; } = null!;
    public Guid AdminId { get; set; }
    public List<Guid> MemberIds { get; set; } = new();
}