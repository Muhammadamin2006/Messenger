using Messenger.DTOs.Messages;
using Messenger.Repositories.MessageRepositories;

namespace Messenger.Services.MessageServices;

public class IncomingMessageService : IIncomingMessageService
{
    private readonly IIncomingMessageRepository _incomingMessageRepository;

    public IncomingMessageService(IIncomingMessageRepository incomingMessageRepository)
    {
        _incomingMessageRepository = incomingMessageRepository;
    }

    public async Task<IncomingMessageDto> GetByIdAsync(Guid id)
    {
        var message = await _incomingMessageRepository.GetByIdAsync(id);
        if (message == null)
            throw new Exception("Message not found");

        return new IncomingMessageDto
        {
            Id = message.Id,
            OutgoingMessageId = message.OutcomingMessageId,
            ReceiverId = message.ReceiverId,
            ReceiverUsername = message.Receiver.Username = "",
            IsRead = message.IsRead,
            OutcomingMessage = new OutcomingMessageDto
            {
                Id = message.OutcomingMessage.Id,
                SenderId = message.OutcomingMessage.SenderId,
                SenderUsername = message.OutcomingMessage.Sender.Username =  "",
                Text = message.OutcomingMessage.Text,
                SentAt = message.OutcomingMessage.SentAt,
                ReceiverUserId = message.OutcomingMessage.ReceiverUserId,
                ReceiverUsername = message.OutcomingMessage.ReceiverUser?.Username,
                ReceiverGroupId = message.OutcomingMessage.ReceiverGroupId,
                ReceiverGroupName = message.OutcomingMessage.ReceiverGroup?.Name
            }
        };
    }

}    


