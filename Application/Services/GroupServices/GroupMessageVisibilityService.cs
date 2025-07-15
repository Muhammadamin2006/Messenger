using Messenger.Domain.Models;
using Messenger.Infrastracture.Repositories;

namespace Messenger.Application.Services.GroupServices;

public class GroupMessageVisibilityService : IGroupMessageVisibilityService
{
    private readonly IGroupMessageRepository _messageRepository;
    private readonly IGroupMessageVisibilityRepository _visibilityRepository;

    public GroupMessageVisibilityService(
        IGroupMessageRepository messageRepository,
        IGroupMessageVisibilityRepository visibilityRepository)
    {
        _messageRepository = messageRepository;
        _visibilityRepository = visibilityRepository;
    }

    public async Task<bool> DeleteMessageForMeAsync(Guid messageId, Guid userId)
    {
        var alreadyHidden = await _visibilityRepository.IsMessageHiddenForUserAsync(messageId, userId);
        if (alreadyHidden)
            return false;

        var visibility = new GroupMessageVisibility
        {
            Id = Guid.NewGuid(),
            MessageId = messageId,
            UserId = userId
        };

        await _visibilityRepository.AddAsync(visibility);
        await _visibilityRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteMessageForEveryoneAsync(Guid messageId, Guid userId)
    {
        var message = await _messageRepository.GetByIdAsync(messageId);

        if (message == null || message.SenderId != userId)
            return false;

        message.IsDeletedForAll = true;
        _messageRepository.Update(message);
        await _messageRepository.SaveChangesAsync();

        return true;
    }
}