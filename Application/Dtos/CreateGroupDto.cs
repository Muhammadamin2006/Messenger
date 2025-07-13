namespace Messenger.Application.Dtos;

public class CreateGroupDto
{
    public string Name { get; set; } = null!;
    public List<Guid> UserIds { get; set; } = new();
}