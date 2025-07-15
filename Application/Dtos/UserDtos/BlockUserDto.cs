namespace Messenger.Application.Dtos;

public class BlockUserDto
{
    public Guid BlockerId { get; set; }
    public Guid BlockedId { get; set; }
}