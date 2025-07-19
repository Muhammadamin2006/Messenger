using Messenger.Application.Dtos.GroupDto;
using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories.GroupRepositories;

public interface IGroupUserRepository : IGenericRepository<GroupUser>
{
    Task<List<GroupUserDto>> GetUsersInGroupChatAsync(Guid groupChatId);

    Task<bool> IsUserInGroupAsync(Guid groupId, Guid userId);
    
    Task RemoveUserFromGroupChatAsync(Guid userId, Guid groupChatId);
    
    Task RemoveAllUsersFromGroupChatAsync(Guid groupChatId);
    
    Task<List<Guid>> GetUserIdsInGroupChatAsync(Guid groupChatId);
    
    Task<bool> IsUserAdminAsync(Guid groupId, Guid userId);
    
    Task<GroupUser?> GetGroupUserAsync(Guid groupUserId);
    
    Task<GroupUser?> GetByUserAndGroupAsync(Guid userId, Guid groupId);

}