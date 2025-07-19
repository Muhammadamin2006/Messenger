using Messenger.Domain.Models;

namespace Messenger.Application.Dtos.GroupDto;

public class GroupDto
{
    public Guid GroupId { get; set; }
    public string GroupName { get; set; } = null!;
    public List<Guid> UserIds { get; set; } = new();
    public List<GroupUserDto> GroupUsers { get; set; } = new List<GroupUserDto>();

}