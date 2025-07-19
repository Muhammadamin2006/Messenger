using Messenger.Application.Dtos;
using Messenger.Application.Dtos.MessageDtos;

namespace Messenger.Application.Services;

public interface IOutgoingService
{
    Task<OutgoingMessageDto> SendMessageAsync(CreateOutgoingMessageDto dto);
    Task<List<OutgoingMessageDto>> GetMessagesBySenderIdAsync(Guid senderId);
    Task DeleteMessageAsync(Guid id);
}

