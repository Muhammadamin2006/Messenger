using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IOutgoingRepository : IGenericRepository<OutgoingMessage>
{
    Task<List<OutgoingMessage>> GetMessagesBySenderIdAsync(Guid senderId);
}