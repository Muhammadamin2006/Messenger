using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IChatMessageVisibilityService
{
    Task<bool> IsMessageHiddenForUserAsync(Guid messageId, Guid userId);

    Task<List<Guid>> GetHiddenMessageIdsAsync(Guid userId, Guid chatId);

    Task HideMessageForUserAsync(Guid messageId, Guid userId);
    
}