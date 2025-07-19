using Messenger.Application.Dtos.GroupDto;

namespace Messenger.Application.Services.GroupServices;

public interface IGroupUserService
{
    Task AddUserToGroupAsync(Guid groupId, Guid userId, Guid addedByUserId, string status = "Member");
    Task LeaveGroupAsync(Guid groupId, string userEmail);
    Task RemoveUserFromGroupByIdAsync(Guid groupId, Guid adminId, Guid targetUserId);
    Task AppointUserToAdminAsync(AppointToAdminDto dto);
}