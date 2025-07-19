using Messenger.Application.Dtos;
using Messenger.Application.Dtos.MessageDtos;

namespace Messenger.Application.Services;

public interface IIncomingService
{
    Task<List<IncomingMessageDto>> GetMessagesByReceiverIdAsync(Guid receiverId);
    Task MarkAsReadAsync(Guid id);
}