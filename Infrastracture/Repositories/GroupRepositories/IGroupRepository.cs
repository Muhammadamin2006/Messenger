using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories.GroupRepositories;

public interface IGroupRepository : IGenericRepository<Group>
{
    Task<Group?> GetGroupChatByIdAsync(Guid groupChatId);
    Task<List<Group>> GetGroupChatsForUserAsync(Guid userId);
    Task<bool> GroupChatExistsAsync(Guid groupChatId);
    Task DeleteGroupChatCompletelyAsync(Guid groupChatId);
    Task<Group?> GetByNameAndUserEmailAsync(string groupName, string userEmail);
    Task<bool> IsGroupNameTakenByUserAsync(string groupName, Guid userId);
    Task<Group?> GetGroupWithUsersAsync(Guid groupId);
    Task SaveChangesAsync();
    Task<bool> IsGroupNameTakenForUserAsync(string groupName, string userEmail);

}