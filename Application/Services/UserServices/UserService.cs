using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Application.Dtos.GroupDto;
using Messenger.Application.Dtos.UserDtos;
using Messenger.Domain.Models;
using Messenger.Domain.Models.UserModels;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;
using Messenger.Infrastracture.Repositories.GroupRepositories;
using SearchContactDto = Messenger.Application.Dtos.SearchContactDto;

namespace Messenger.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IGroupRepository _groupRepository;

    public UserService(IUserRepository userRepository, IMapper mapper,  IGroupRepository groupRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _groupRepository = groupRepository;
    }

    public async Task<UserDto> RegisterUserAsync(RegistrationDto dto)
    {
        var isTaken = await _userRepository.IsEmailTakenAsync(dto.Email);
        if (isTaken)
            throw new Exception("Email уже используется.");

        var user = _mapper.Map<User>(dto);
        user.UserCreatedAt = DateTime.UtcNow;

        await _userRepository.AddAsync(user);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await _userRepository.IsEmailTakenAsync(email);
    }

    public async Task<List<SearchContactDto>> SearchUsersByNameAsync(string name)
    {
        var users = await _userRepository.SearchUsersByNameAsync(name);

        var result = users.Select(u => new SearchContactDto
        {
            Username = u.Username,
            Email = u.Email
        }).ToList();

        return result;
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("Пользователь не найден.");

        _userRepository.Delete(user);
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return _mapper.Map<List<UserDto>>(users);
    }
    
    public async Task<List<GroupDto>> GetUserGroupsAsync(Guid userId)
    {
        var groups = await _groupRepository.GetGroupChatsForUserAsync(userId);
        return _mapper.Map<List<GroupDto>>(groups);
    }
}