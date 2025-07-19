using Messenger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IChatUserService
{
    Task<List<ChatUser>> GetUsersInChatAsync(Guid chatId);

    Task AddUserToChatAsync(Guid chatId, Guid userId, string status = "Member");

    Task<bool> IsUserInChatAsync(Guid userId, Guid chatId);

    Task RemoveUserFromChatAsync(Guid userId, Guid chatId);
    
    Task<List<Guid>> GetUserIdsInChatAsync(Guid chatId);
}