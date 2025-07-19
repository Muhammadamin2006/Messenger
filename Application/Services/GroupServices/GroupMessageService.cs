using AutoMapper;
using Messenger.Application.Dtos.GroupDto;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Repositories;
using Messenger.Infrastracture.Repositories.GroupRepositories;

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

    
    public async Task<GroupMessageDto> SendMessageAsync(CreateGroupMessageDto dto, Guid senderId)
    {
        var isMember = await _groupUserRepository.IsUserInGroupAsync(dto.GroupId, senderId);
        if (!isMember)
            throw new Exception("You don't exist in this group");

        var message = new GroupMessage
        {
            GroupMessageId = Guid.NewGuid(),
            GroupId = dto.GroupId,
            GroupSenderId = senderId,
            Text = dto.Text,
            GroupMessageSentAt = DateTime.UtcNow
        };

        await _groupMessageRepository.AddAsync(message);
        await _groupMessageRepository.SaveChangesAsync();

        var sender = await _groupUserRepository.GetByUserAndGroupAsync(senderId, dto.GroupId);
        
        return new GroupMessageDto
        {
            GroupMessageId = message.GroupMessageId,
            Text = message.Text,
            SenderName = sender?.User.Username ?? "Unknown",
            GroupMessageSentAt = message.GroupMessageSentAt
        };
    }

    
    public async Task<List<GroupMessageDto>> GetMessagesByGroupIdAsync(Guid groupId, Guid userId)
    {
        var messages = await _groupMessageRepository.GetMessagesByGroupChatIdAsync(groupId);

        var visibleMessages = new List<GroupMessageDto>();

        foreach (var message in messages)
        {
            var isDeleted = await _groupMessageVisibilityRepository.IsMessageHiddenForUserAsync(message.GroupMessageId, userId);
            if (isDeleted) continue;

            visibleMessages.Add(new GroupMessageDto
            {
                GroupMessageId = message.GroupMessageId,
                Text = message.Text,
                SenderName = message.GroupSender?.Username ?? "Unknown",
                GroupMessageSentAt = message.GroupMessageSentAt,
                GroupMessageEditedAt = message.GroupMessageEditedAt
            });
        }

        return visibleMessages
            .OrderBy(m => m.GroupMessageSentAt)
            .ToList();
    }

    public async Task<GroupMessageDto> EditMessageAsync(Guid messageId, Guid editorId, string newText)
    {
        var message = await _groupMessageRepository.GetMessageByIdAsync(messageId);
        if (message == null)
            throw new Exception("Message not found");

        if (message.GroupSenderId != editorId)
            throw new Exception("You can't edit this message");

        message.Text = newText;
        message.IsEdited = true;
        message.GroupMessageEditedAt = DateTime.UtcNow;

        await _groupMessageRepository.SaveChangesAsync();

        var sender = await _groupUserRepository.GetByIdAsync(editorId);

        return new GroupMessageDto
        {
            GroupMessageId = message.GroupMessageId,
            Text = message.Text,
            SenderName = sender?.GroupUsername ?? "Unknown",
            GroupMessageSentAt = message.GroupMessageSentAt,
            IsEdited = true,
            EditedAt = message.GroupMessageEditedAt
        };
    }

    
    public async Task DeleteMessageAsync(Guid messageId, Guid currentUserId, bool deleteForAll)
    {
        var message = await _groupMessageRepository.GetMessageByIdAsync(messageId);
        if (message == null)
            throw new Exception("Message not found");

        if (message.GroupSenderId == currentUserId)
        {
            if (deleteForAll)
            {
                _groupMessageRepository.Delete(message);
            }
            else
            {
                await _groupMessageVisibilityRepository.HideMessageForUserAsync(messageId, currentUserId);
            }
        }
        else
        {
            await _groupMessageVisibilityRepository.HideMessageForUserAsync(messageId, currentUserId);
        }

        await _groupMessageRepository.SaveChangesAsync();
    }
    
    }
