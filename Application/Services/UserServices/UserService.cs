using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;

namespace Messenger.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserBlocksRepository  _userBlocksRepository;
    private readonly MessengerContext _context;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IUserBlocksRepository userBlocksRepository,
        MessengerContext context, IMapper mapper)
    {
        _userRepository = userRepository;
        _userBlocksRepository = userBlocksRepository;
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> RegisterUserAsync(RegisterUserDto dto)
    {
        var emailTaken = await _userRepository.IsEmailTakenAsync(dto.Email);
        if (emailTaken)
            throw new Exception("Email уже используется");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.UserName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            PasswordHash = passwordHash
        };

        Console.WriteLine($"Регистрация пользователя с Id: {user.Id}, Email: {user.Email}, PasswordHash: {user.PasswordHash}");
        await _userRepository.AddAsync(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
        
    }

    
    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("Пользователь не найден");

        await _userRepository.DeleteAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<SearchContactDto?> FindUserByPhoneNumberAsync(string phoneNumber)
    {
        var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber);
        if (user == null) return null;

        return _mapper.Map<SearchContactDto>(user);
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var getalll = await _userRepository.GetAllUsersAsync();
        return _mapper.Map<List<UserDto>>(getalll);
    }

    
}