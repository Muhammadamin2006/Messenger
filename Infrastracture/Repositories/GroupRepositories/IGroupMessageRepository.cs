using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IGroupMessageRepository : IGenericRepository<GroupMessage>
{
    Task<List<GroupMessage>> GetMessagesByGroupIdAsync(Guid groupId);
    Task<GroupMessage?> GetByIdWithSenderAsync(Guid messageId);
    Task SaveChangesAsync();
}