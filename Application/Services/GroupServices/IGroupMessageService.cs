using Messenger.Application.Dtos.GroupDto;

namespace Messenger.Application.Services.GroupServices;

public interface IGroupMessageService
{
    Task<GroupMessageDto> SendMessageAsync(CreateGroupMessageDto dto, Guid senderId);
    Task DeleteMessageAsync(Guid messageId, Guid currentUserId, bool deleteForAll);
    Task<GroupMessageDto> EditMessageAsync(Guid messageId, Guid editorId, string newText);
    Task<List<GroupMessageDto>> GetMessagesByGroupIdAsync(Guid groupId, Guid userId);
}