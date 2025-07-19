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

    public async Task<bool> IsMessageHiddenForUserAsync(Guid messageId, Guid userId)
    {
        return await _visibilityRepository.IsMessageHiddenForUserAsync(messageId, userId);
    }

    public async Task HideMessageForUserAsync(Guid messageId, Guid userId)
    {
        bool alreadyHidden = await _visibilityRepository.IsMessageHiddenForUserAsync(messageId, userId);
        if (alreadyHidden)
            return;

        var visibility = new GroupMessageVisibility
        {
            GroupMessageVisibilityMessageId = Guid.NewGuid(),
            GroupHidMessageId = messageId,
            HidByUserId = userId,
            GroupMessageHiddenAt = DateTime.UtcNow
        };

        await _visibilityRepository.AddAsync(visibility);
        await _visibilityRepository.SaveChangesAsync();
    }

    public async Task<List<Guid>> GetHiddenMessageIdsAsync(Guid userId, Guid groupChatId)
    {
        return await _visibilityRepository.GetHiddenMessageIdsAsync(userId, groupChatId);
    }
}
    
