using Messenger.Application.Dtos.GroupDto;

namespace Messenger.Application.Services.GroupServices;

public interface IGroupMessageService 
{
    Task<GroupMessageDto> SendMessageAsync(SendGroupMessageDto dto);
    Task<GroupMessageDto> EditMessageAsync(EditGroupMessageDto dto);

    Task<List<GroupMessageDto>> GetMessagesInGroupAsync(Guid groupId, Guid userId);

}