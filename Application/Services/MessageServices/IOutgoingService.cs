using Messenger.Application.Dtos;

namespace Messenger.Application.Services;

public interface IOutgoingService
{
    Task<OutgoingMessageDto> SendMessageAsync(CreateOutgoingMessageDto dto);
    Task<List<OutgoingMessageDto>> GetMessagesBySenderIdAsync(Guid senderId);
    Task DeleteMessageAsync(Guid id);
}

