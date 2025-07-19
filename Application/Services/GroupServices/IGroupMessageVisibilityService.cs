using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Application.Services.GroupServices;

public interface IGroupMessageVisibilityService
{
    Task<bool> IsMessageHiddenForUserAsync(Guid messageId, Guid userId);
    Task HideMessageForUserAsync(Guid messageId, Guid userId);
    Task<List<Guid>> GetHiddenMessageIdsAsync(Guid userId, Guid groupChatId);
}
 
    
