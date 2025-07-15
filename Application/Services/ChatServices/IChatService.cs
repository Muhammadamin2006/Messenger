using Messenger.Application.Dtos;

namespace Messenger.Application.Services;

public interface IChatService
{
    Task<ChatDto> CreateBetweenUsersChatAsync(Guid firstUserId, Guid secondUserId);
    Task<ChatDto?> GetBetweenUsersChatAsync(Guid firstUserId, Guid secondUserId);
    Task<List<ChatDto>> GetChatsForUserAsync(Guid userId);


}