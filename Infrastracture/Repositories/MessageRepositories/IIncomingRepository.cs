using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Repositories;

public interface IIncomingRepository : IGenericRepository<IncomingMessage>
{
    Task<List<IncomingMessage>> GetByReceiverIdAsync(Guid receiverId);
}