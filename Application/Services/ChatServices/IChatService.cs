using Messenger.Application.Dtos;
using Messenger.Application.Dtos.ChatDtos;
using Messenger.Domain.Models;

namespace Messenger.Application.Services;

public interface IChatService
{
    Task<ChatDto> CreateChatBetweenUsersAsync(Guid firstUserId, Guid secondUserId);

    Task<ChatDto?> GetChatByIdAsync(Guid chatId, Guid requestingUserId);

    Task<List<ChatDto>> GetChatsForUserAsync(Guid userId);

    Task DeleteChatAsync(Guid chatId, Guid requestingUserId);


}