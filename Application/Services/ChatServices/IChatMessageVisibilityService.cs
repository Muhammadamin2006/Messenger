namespace Messenger.Application.Services;

public interface IChatMessageVisibilityService
{
    Task<bool> DeleteMessageForMeAsync(Guid messageId, Guid userId);
    Task<bool> DeleteMessageForEveryoneAsync(Guid messageId, Guid userId);
}