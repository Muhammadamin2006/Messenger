namespace Messenger.Application.Dtos.GroupDto;

public class AppointToAdminDto
{
    public Guid GroupId { get; set; }
    public Guid TargetUserId { get; set; }
    public string RequesterEmail { get; set; } = null!;
}