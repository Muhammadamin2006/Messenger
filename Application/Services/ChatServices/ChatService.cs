using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Application.Dtos.ChatDtos;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;
using Messenger.Infrastracture.Repositories.ChatRepositories;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Application.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository; 
    private readonly IChatUserRepository _chatUserRepository;
    private readonly IMapper _mapper;

    
    public ChatService(IChatRepository chatRepository, IChatUserRepository chatUserRepository, IMapper mapper)
    {
        _chatRepository = chatRepository;
        _chatUserRepository = chatUserRepository;
        _mapper = mapper;
    }

   
    public async Task<ChatDto> CreateChatBetweenUsersAsync(Guid firstUserId, Guid secondUserId)
    {
        
        if (firstUserId == secondUserId)
            throw new ArgumentException("Нельзя создать чат с самим собой.");

        
        bool exists = await _chatRepository.ChatExistsAsync(firstUserId, secondUserId);
        if (exists)
            throw new InvalidOperationException("Чат между этими пользователями уже существует.");

        
        var chat = new Chat
        {
            ChatId = Guid.NewGuid(),
            ChatCreatedAt = DateTime.UtcNow,

            
            ChatUsers = new List<ChatUser>
            {
                new ChatUser { UserId = firstUserId },
                new ChatUser { UserId = secondUserId }
            }
        };

        
        await _chatRepository.AddAsync(chat);
        await _chatRepository.SaveChangesAsync(chat);

        
        return _mapper.Map<ChatDto>(chat);
    }

    
    public async Task<ChatDto?> GetChatByIdAsync(Guid chatId, Guid requestingUserId)
    {
        
        var chat = await _chatRepository.GetChatByIdAsync(chatId);
        if (chat == null)
            return null; 

        
        bool isUserInChat = await _chatUserRepository.IsUserInChatAsync(requestingUserId, chatId);
        if (!isUserInChat)
            throw new UnauthorizedAccessException("Пользователь не участник этого чата.");

        
        return _mapper.Map<ChatDto>(chat);
    }

    
    public async Task<List<ChatDto>> GetChatsForUserAsync(Guid userId)
    {
        
        var chats = await _chatRepository.GetChatsForUserAsync(userId);
        
        return _mapper.Map<List<ChatDto>>(chats);
    }

    
    public async Task DeleteChatAsync(Guid chatId, Guid requestingUserId)
    {
        
        bool isUserInChat = await _chatUserRepository.IsUserInChatAsync(requestingUserId, chatId);
        if (!isUserInChat)
            throw new UnauthorizedAccessException("Пользователь не участник этого чата.");

        
        await _chatRepository.DeleteChatCompletelyAsync(chatId);
    }
}