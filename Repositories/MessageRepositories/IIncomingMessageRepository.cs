using Messenger.Models;

namespace Messenger.Repositories.MessageRepositories;

public interface IIncomingMessageRepository
{
    Task<IncomingMessage> GetByIdAsync(Guid id);
}