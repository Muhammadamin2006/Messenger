namespace Messenger.Application.Dtos;

public class GroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Guid> UserIds { get; set; } = new();

}