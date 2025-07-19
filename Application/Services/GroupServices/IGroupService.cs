using Messenger.Application.Dtos.GroupDto;
using Messenger.Domain.Models;

namespace Messenger.Application.Services.GroupServices;

public interface IGroupService
{
    Task<GroupDto> CreateGroupAsync(CreateGroupDto dto, string creatorEmail, Guid userId);
    // Task<List<GroupUserDto>> GetGroupMembersAsync(Guid groupId, string currentUserEmail);
    Task<List<GroupUsersWithStatusDto>> GetGroupUsersWithStatusAsync(Guid groupId);
    Task DeleteGroupAsync(Guid groupId, Guid initiatorId);
    Task<bool> AssignNewAdminAsync(Guid groupId, Guid newAdminUserId, string requesterEmail);
    Task<GroupDto?> GetGroupByIdAsync(Guid groupId);
    Task RenameGroupAsync(Guid groupId, string newName, Guid userId);

}