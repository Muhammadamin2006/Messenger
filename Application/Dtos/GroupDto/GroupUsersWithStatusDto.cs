namespace Messenger.Application.Dtos.GroupDto;

public class GroupUsersWithStatusDto
{
    public Guid GroupUserId { get; set; }
    public string GroupUserName { get; set; } = null!;
    public string GroupUserEmail { get; set; } = null!;
    public string GroupUserStatus { get; set; } = null!;
}