using Messenger.DTOs.Messages;
using Messenger.Infrastractures.Database;
using Messenger.Models;
using Messenger.Repositories.MessageRepositories;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Services.MessageServices;

public class OutcomingMessageService : IOutcomingMessageService
{
    private readonly MessengerContext _context;
    private readonly IOutcomingMessageRepository  _outcomingMessageRepository;
    private readonly IIncomingMessageRepository  _incomingMessageRepository;

    public OutcomingMessageService(IOutcomingMessageRepository outcomingMessageRepository, IIncomingMessageRepository incomingMessageRepository)
    {
        
        _outcomingMessageRepository = outcomingMessageRepository;
        _incomingMessageRepository = incomingMessageRepository;
        
    }
    
    public async Task<OutcomingMessageDto> CreateAsync(CreateOutcomingMessageDto createOutcomingMessageDto)
    {
        var outcomingmessage = new OutcomingMessage()
        {
            Id = Guid.NewGuid(),
            SenderId = createOutcomingMessageDto.SenderId,
            Text = createOutcomingMessageDto.Text,
            SentAt = DateTime.UtcNow,
            ReceiverUserId = createOutcomingMessageDto.ReceiverUserId,
            ReceiverGroupId = createOutcomingMessageDto.ReceiverGroupId,
            IncomingMessages = new List<IncomingMessage>()
        };
        
        if (createOutcomingMessageDto.ReceiverUserId.HasValue)
        {
            outcomingmessage.IncomingMessages.Add(new IncomingMessage
            {
                Id = Guid.NewGuid(),
                OutcomingMessageId = outcomingmessage.Id,
                ReceiverId = createOutcomingMessageDto.ReceiverUserId.Value,
                IsRead = false
            });
        }
        
        else if (createOutcomingMessageDto.ReceiverGroupId.HasValue)
        {
            var members = await _outcomingMessageRepository.GetGroupMemberIdsAsync(createOutcomingMessageDto.ReceiverGroupId.Value);
            
            foreach (var memberId in members)
            {
                outcomingmessage.IncomingMessages.Add(new IncomingMessage
                {
                    Id = Guid.NewGuid(),
                    OutcomingMessageId = outcomingmessage.Id,
                    ReceiverId = memberId,
                    IsRead = false
                });
            }
        }
        
        else
        {
            throw new ArgumentException("Receiver must be a user or a group.");
        }

        var saved = await _outcomingMessageRepository.CreateAsync(outcomingmessage);

        return new OutcomingMessageDto
        {
            Id = saved.Id,
            SenderId = saved.SenderId,
            Text = saved.Text,
            SentAt = saved.SentAt,
            ReceiverUserId = saved.ReceiverUserId,
            ReceiverGroupId = saved.ReceiverGroupId
        };
        
    }
}


