using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Application.Dtos.MessageDtos;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;
using Messenger.Infrastracture.Repositories.GroupRepositories;


namespace Messenger.Application.Services;


public class OutgoingService : IOutgoingService
{
    private readonly IOutgoingRepository _outgoingRepository;
    private readonly IIncomingRepository _incomingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGroupUserRepository _groupUserRepository;
    private readonly IMapper _mapper;
    private readonly MessengerContext _context;
    private readonly IGroupMessageRepository _groupMessageRepository;

    public OutgoingService(
        IOutgoingRepository outgoingRepository,
        IIncomingRepository incomingRepository,
        IUserRepository userRepository,
        IGroupUserRepository groupUserRepository,
        IMapper mapper, MessengerContext  context)
    {
        _outgoingRepository = outgoingRepository;
        _incomingRepository = incomingRepository;
        _userRepository = userRepository;
        _groupUserRepository = groupUserRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<OutgoingMessageDto> SendMessageAsync(CreateOutgoingMessageDto dto)
    {
        var sender = await _userRepository.GetByIdAsync(dto.SenderId);
        if (sender == null)
            throw new Exception("Отправитель не найден");

        if (dto.ReceiverUserId.HasValue && dto.ReceiverUserId.Value == dto.SenderId)
            throw new Exception("Нельзя отправить сообщение самому себе");

        var message = new OutgoingMessage
        {
            Id = Guid.NewGuid(),
            SenderId = dto.SenderId,
            Text = dto.Text,
            SentAt = DateTime.UtcNow,
            ReceiverUserId = dto.ReceiverUserId,
            ReceiverGroupId = dto.ReceiverGroupId
        };

        await _outgoingRepository.AddAsync(message);

        var incomingMessages = new List<IncomingMessage>();

        if (dto.ReceiverUserId.HasValue)
        {
            var receiver = await _userRepository.GetByIdAsync(dto.ReceiverUserId.Value);
            if (receiver == null)
                throw new Exception("Получатель не найден");

            incomingMessages.Add(new IncomingMessage
            {
                Id = Guid.NewGuid(),
                OutgoingMessageId = message.Id,
                ReceiverId = receiver.UserId,
                IsRead = false
            });
        }
        else if (dto.ReceiverGroupId.HasValue)
        {
            var group = await _groupUserRepository.GetByIdAsync(dto.ReceiverGroupId.Value);
            if (group == null)
                throw new Exception("Группа не найдена");

            var isMember = group.GroupUsers.Any(ug => ug.UserId == dto.SenderId);
            if (!isMember)
                throw new Exception("Вы не состоите в группе");

            foreach (var user in group.GroupUsers.Where(x => x.UserId != dto.SenderId))
            {
                incomingMessages.Add(new IncomingMessage
                {
                    Id = Guid.NewGuid(),
                    OutgoingMessageId = message.Id,
                    ReceiverId = user.UserId,
                    IsRead = false
                });
            }
        }
        else
        {
            throw new Exception("Нужно указать получателя: UserId или GroupId");
        }

        foreach (var incoming in incomingMessages)
        {
            await _incomingRepository.AddAsync(incoming);
        }
        await _context.SaveChangesAsync();

        return _mapper.Map<OutgoingMessageDto>(message);
    }

    public async Task<List<OutgoingMessageDto>> GetMessagesBySenderIdAsync(Guid senderId)
    {
        var messages = await _outgoingRepository.GetMessagesBySenderIdAsync(senderId);
        return _mapper.Map<List<OutgoingMessageDto>>(messages);
    }


    public async Task DeleteMessageAsync(Guid id)
    {
        var message = await _outgoingRepository.GetByIdAsync(id);
        if (message == null)
            throw new Exception("Сообщение не найдено");

        _outgoingRepository.Delete(message);
    }
}