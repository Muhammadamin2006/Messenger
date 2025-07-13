using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IGroupRepository : IGenericRepository<Group>
{
    Task<Group?> GetByIdWithUsersAsync(Guid groupId);
}