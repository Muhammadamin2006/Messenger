using Messenger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Messenger.Application.Dtos.ChatDtos;

public interface IChatMessageService
{
    Task<List<ChatMessageDto>> GetMessagesByChatIdAsync(Guid chatId);

    Task<ChatMessageDto?> GetMessageByIdAsync(Guid messageId);

    Task EditMessageAsync(Guid messageId, Guid userId, string newText);

    Task DeleteMessagesByChatIdAsync(Guid chatId);

    Task<ChatMessageDto> SendMessageAsync(Guid chatId, Guid senderId, string text);
}