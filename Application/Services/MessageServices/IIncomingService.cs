using Messenger.Application.Dtos;

namespace Messenger.Application.Services;

public interface IIncomingService
{
    Task<List<IncomingMessageDto>> GetMessagesByReceiverIdAsync(Guid receiverId);
    Task MarkAsReadAsync(Guid id);
}