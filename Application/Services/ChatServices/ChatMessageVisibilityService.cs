using Messenger.Domain.Models;
using Messenger.Infrastracture.Repositories;

namespace Messenger.Application.Services;

public class ChatMessageVisibilityService : IChatMessageVisibilityService
{
    private readonly IChatMessageVisibilityRepository _visibilityRepository;

    public ChatMessageVisibilityService(IChatMessageVisibilityRepository visibilityRepository)
    {
        _visibilityRepository = visibilityRepository;
    }

    public async Task<bool> IsMessageHiddenForUserAsync(Guid messageId, Guid userId)
    {
        return await _visibilityRepository.IsMessageHiddenForUserAsync(messageId, userId);
    }

    public async Task<List<Guid>> GetHiddenMessageIdsAsync(Guid userId, Guid chatId)
    {
        return await _visibilityRepository.GetHiddenMessageIdsAsync(userId, chatId);
    }

    public async Task HideMessageForUserAsync(Guid messageId, Guid userId)
    {
        bool alreadyHidden = await _visibilityRepository.IsMessageHiddenForUserAsync(messageId, userId);
        if (alreadyHidden) return;

        var hidden = new ChatMessageVisibility
        {
            ChatMessageVisibilityId = Guid.NewGuid(),
            HiddenMessageId = messageId,
            HidUserId = userId
        };

        await _visibilityRepository.AddAsync(hidden);
        await _visibilityRepository.SaveChangesAsync();
    }

 
}