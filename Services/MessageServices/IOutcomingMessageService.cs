using Messenger.DTOs.Messages;

namespace Messenger.Services.MessageServices;

public interface IOutcomingMessageService
{
    Task<OutcomingMessageDto> CreateAsync(CreateOutcomingMessageDto createOutcomingMessageDto);
    
}