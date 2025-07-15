using Messenger.Domain.Models;
using Messenger.Infrastracture.Repositories;

namespace Messenger.Application.Services;

public class ChatMessageVisibilityService : IChatMessageVisibilityService
{
    
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IChatMessageVisibilityRepository _chatMessageVisibilityRepository;

    public ChatMessageVisibilityService(IChatMessageRepository chatMessageRepository,  IChatMessageVisibilityRepository chatMessageVisibilityRepository)
    {
        _chatMessageRepository = chatMessageRepository;
        _chatMessageVisibilityRepository = chatMessageVisibilityRepository;
    }
    
    public async Task<bool> DeleteMessageForMeAsync(Guid messageId, Guid userId)
    {
        var message = await _chatMessageRepository.GetByIdAsync(messageId);
        if (message == null) return false;

        var alreadyHidden = await _chatMessageVisibilityRepository.IsMessageHiddenForUserAsync(messageId, userId);
        if (alreadyHidden) return true;

        var visibility = new ChatMessageVisibility
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MessageId = messageId
        };

        await _chatMessageVisibilityRepository.AddAsync(visibility);
        await _chatMessageVisibilityRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteMessageForEveryoneAsync(Guid messageId, Guid userId)
    {
        var message = await _chatMessageRepository.GetByIdAsync(messageId);
        if (message == null || message.SenderId != userId) return false;

        message.IsDeleted = true;
        _chatMessageRepository.Update(message);
        await _chatMessageRepository.SaveChangesAsync(message);

        return true;
    }
}