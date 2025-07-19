using Messenger.Domain.Models;

namespace Messenger.Application.Dtos.GroupDto;

public class GroupUserDto
{
    public Guid GroupUserId { get; set; }
    public string GroupUsername { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AdminName { get; set; } = null!;
    public DateTime GroupUserJoinedAt { get; set; }
    public Guid GroupId { get; set; }
    public string Status { get; set; } = null!; 
    public bool IsAdmin  { get; set; } =  false;
    
}