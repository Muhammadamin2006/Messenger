namespace Messenger.Application.Dtos.UserDtos;

public class BlockUserDto
{
    public Guid BlockerId { get; set; }
    public Guid BlockedUserId { get; set; }
}