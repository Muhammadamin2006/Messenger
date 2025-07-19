using AutoMapper;
using Messenger.Application.Dtos.UserDtos;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Repositories.ChatRepositories;

namespace Messenger.Application.Services;

public class ChatUserService : IChatUserService
{
    private readonly IChatUserRepository _chatUserRepository;
    private readonly IMapper _mapper;

    public ChatUserService(IChatUserRepository chatUserRepository, IMapper mapper)
    {
        _chatUserRepository = chatUserRepository;
        _mapper = mapper;
    }

    public async Task<List<ChatUser>> GetUsersInChatAsync(Guid chatId)
    {
        var chatUsers = await _chatUserRepository.GetUsersInChatAsync(chatId);
        var users = chatUsers.Select(cu => cu.User).ToList();
        return _mapper.Map<List<ChatUser>>(users);
    }

    public async Task<bool> IsUserInChatAsync(Guid userId, Guid chatId)
    {
        return await _chatUserRepository.IsUserInChatAsync(userId, chatId);
    }
    
    public async Task AddUserToChatAsync(Guid chatId, Guid userId, string status = "Member")
    {
        bool isInChat = await _chatUserRepository.IsUserInChatAsync(userId, chatId);
        if (isInChat)
            throw new InvalidOperationException("Пользователь уже в чате.");

        var chatUser = new ChatUser
        {
            ChatId = chatId,
            UserId = userId,
            Status = status
        };

        await _chatUserRepository.AddAsync(chatUser);
        await _chatUserRepository.SaveChangesAsync();
    }

    

    public async Task RemoveUserFromChatAsync(Guid userId, Guid chatId)
    {
        await _chatUserRepository.RemoveUserFromChatAsync(userId, chatId);
        await _chatUserRepository.SaveChangesAsync();
    }
    

    public async Task<List<Guid>> GetUserIdsInChatAsync(Guid chatId)
    {
        return await _chatUserRepository.GetUserIdsInChatAsync(chatId);
    }
}