using Messenger.Models;

namespace Messenger.Repositories.MessageRepositories;

public interface IOutcomingMessageRepository
{
    Task<OutcomingMessage> CreateAsync(OutcomingMessage outcomingMessage);
    Task<List<Guid>> GetGroupMemberIdsAsync(Guid groupId);
}