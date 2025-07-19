namespace Messenger.Application.Dtos.GroupDto;

public class GroupMembersDto
{
    public Guid GroupId { get; set; }
    public List<GroupUserDto> GroupMembers { get; set; } = new();
}