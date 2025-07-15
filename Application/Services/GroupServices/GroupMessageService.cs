using AutoMapper;
using Messenger.Application.Dtos.GroupDto;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Repositories;

namespace Messenger.Application.Services.GroupServices;

public class GroupMessageService : IGroupMessageService
{
    private readonly IGroupMessageRepository _groupMessageRepository;
    private readonly IGroupUserRepository _groupUserRepository;
    private readonly IMapper _mapper;
    private readonly IGroupMessageVisibilityRepository _groupMessageVisibilityRepository;

    public GroupMessageService(IGroupMessageRepository groupMessageRepository, IGroupUserRepository groupUserRepository, IMapper mapper,  
        IGroupMessageVisibilityRepository groupMessageVisibilityRepository)
    {
        _groupMessageRepository = groupMessageRepository;
        _groupUserRepository = groupUserRepository;
        _mapper = mapper;
        _groupMessageVisibilityRepository = groupMessageVisibilityRepository;
    }

    public async Task<GroupMessageDto> SendMessageAsync(SendGroupMessageDto dto)
    {
        var isMember = await _groupUserRepository.IsUserInGroupAsync(dto.SenderId, dto.GroupId);
        if (!isMember)
            throw new InvalidOperationException("Пользователь не состоит в группе");

        var message = _mapper.Map<GroupMessage>(dto);
        message.GroupMessageId = Guid.NewGuid();
        message.SentAt = DateTime.UtcNow;

        await _groupMessageRepository.AddAsync(message);
        await _groupMessageRepository.SaveChangesAsync();

        var withSender = await _groupMessageRepository.GetByIdWithSenderAsync(message.GroupMessageId);
        return _mapper.Map<GroupMessageDto>(withSender);
    }
    
    public async Task<GroupMessageDto> EditMessageAsync(EditGroupMessageDto dto)
    {
        var message = await _groupMessageRepository.GetByIdWithSenderAsync(dto.MessageId);

        if (message == null)
            throw new InvalidOperationException("Сообщение не найдено");

        if (message.SenderId != dto.SenderId)
            throw new InvalidOperationException("Ты не автор этого сообщения");

        message.Text = dto.NewText;
        message.EditedAt = DateTime.UtcNow;

        _groupMessageRepository.Update(message);
        await _groupMessageRepository.SaveChangesAsync();

        return _mapper.Map<GroupMessageDto>(message);
    }

        public async Task<List<GroupMessageDto>> GetMessagesInGroupAsync(Guid groupId, Guid userId)
        {
            var allMessages = await _groupMessageRepository.GetMessagesByGroupIdAsync(groupId);

            var hiddenMessageIds = await _groupMessageVisibilityRepository.GetHiddenMessageIdsForUserAsync(userId);

            var visibleMessages = allMessages
                .Where(msg => !msg.IsDeletedForAll && !hiddenMessageIds.Contains(msg.GroupMessageId))
                .ToList();

            return _mapper.Map<List<GroupMessageDto>>(visibleMessages);
        }
    }
