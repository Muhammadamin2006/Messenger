using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Application.Services.GroupServices;

public interface IGroupMessageVisibilityService
{
    Task<bool> DeleteMessageForMeAsync(Guid messageId, Guid userId);
    Task<bool> DeleteMessageForEveryoneAsync(Guid messageId, Guid userId);
}
 
    
