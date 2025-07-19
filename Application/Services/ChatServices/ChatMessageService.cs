using AutoMapper;
using Messenger.Application.Dtos.ChatDtos;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Repositories;
using Messenger.Infrastracture.Repositories.ChatRepositories;

namespace Messenger.Application.Services;

public class ChatMessageService : IChatMessageService
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IChatUserRepository _chatUserRepository;
    private readonly IMapper _mapper;

    public ChatMessageService(
        IChatMessageRepository chatMessageRepository,
        IChatUserRepository chatUserRepository,
        IMapper mapper)
    {
        _chatMessageRepository = chatMessageRepository;
        _chatUserRepository = chatUserRepository;
        _mapper = mapper;
    }

    public async Task<List<ChatMessageDto>> GetMessagesByChatIdAsync(Guid chatId)
    {
        var messages = await _chatMessageRepository.GetMessagesByChatIdAsync(chatId);
        return _mapper.Map<List<ChatMessageDto>>(messages);
    }

    public async Task<ChatMessageDto?> GetMessageByIdAsync(Guid messageId)
    {
        var message = await _chatMessageRepository.GetMessageByIdAsync(messageId);
        if (message == null) return null;
        return _mapper.Map<ChatMessageDto>(message);
    }

    public async Task EditMessageAsync(Guid messageId, Guid userId, string newText)
    {
        var message = await _chatMessageRepository.GetMessageByIdAsync(messageId);
        if (message == null)
            throw new Exception("Сообщение не найдено");

        if (message.SenderId != userId)
            throw new UnauthorizedAccessException("Редактировать можно только свои сообщения");

        message.Text = newText;
        message.EditedAt = DateTime.UtcNow;

        await _chatMessageRepository.SaveChangesAsync();
    }

    public async Task DeleteMessagesByChatIdAsync(Guid chatId)
    {
        await _chatMessageRepository.DeleteMessagesByChatIdAsync(chatId);
    }

    public async Task<ChatMessageDto> SendMessageAsync(Guid chatId, Guid senderId, string text)
    {
        bool isInChat = await _chatUserRepository.IsUserInChatAsync(senderId, chatId);
        if (!isInChat)
            throw new UnauthorizedAccessException("Пользователь не является участником чата");

        var message = new ChatMessage
        {
            MessageId = Guid.NewGuid(),
            ChatId = chatId,
            SenderId = senderId,
            Text = text,
            SentAt = DateTime.UtcNow
        };

        await _chatMessageRepository.AddAsync(message);
        await _chatMessageRepository.SaveChangesAsync();

        return _mapper.Map<ChatMessageDto>(message);
    }
}