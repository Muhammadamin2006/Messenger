using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Application.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ChatService(IChatRepository chatRepository, IUserRepository userRepository, IMapper mapper)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ChatDto> CreateBetweenUsersChatAsync(Guid firstUserId, Guid secondUserId)
    {
        if (firstUserId == secondUserId)
            throw new Exception("Нельзя создать чат с самим собой");

        var user1 = await _userRepository.GetByIdAsync(firstUserId);
        var user2 = await _userRepository.GetByIdAsync(secondUserId);

        if (user1 == null || user2 == null)
            throw new Exception("Один из пользователей не найден");

        var existingChat = await _chatRepository.GetChatBetweenUsersAsync(firstUserId, secondUserId);
        if (existingChat != null)
            return _mapper.Map<ChatDto>(existingChat);

        var newChat = new Chat
        {
            ChatId = Guid.NewGuid(),
            FirstUserId = firstUserId,
            SecondUserId = secondUserId,
            CreatedAt = DateTime.UtcNow
        };

        await _chatRepository.AddAsync(newChat);
        await _chatRepository.SaveChangesAsync(); 

        return _mapper.Map<ChatDto>(newChat);
    }

    public async Task<ChatDto?> GetBetweenUsersChatAsync(Guid firstUserId, Guid secondUserId)
    {
        var chat = await _chatRepository.GetChatBetweenUsersAsync(firstUserId, secondUserId);
        return chat == null ? null : _mapper.Map<ChatDto>(chat);
    }

    public async Task<List<ChatDto>> GetChatsForUserAsync(Guid userId)
    {
        var chats = await _chatRepository.GetChatsForUserAsync(userId);
        return _mapper.Map<List<ChatDto>>(chats);
    }
}