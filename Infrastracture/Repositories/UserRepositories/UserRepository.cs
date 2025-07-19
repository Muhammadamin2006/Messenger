using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Application.Services;
using Messenger.Domain.Models;
using Messenger.Domain.Models.UserModels;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly MessengerContext _context;
    private readonly DbSet<User> _users;
    private readonly UserRepository _usersRepository;
    private readonly IMapper _mapper;

    public UserRepository(MessengerContext context, UserRepository userRepository) : base(context)
    {
        _context = context;
        _users = context.Users;
        _usersRepository = userRepository;
    }

    public async Task AddAsync(User user)
    {
        if (user.UserId == Guid.Empty)
            user.UserId = Guid.NewGuid();

        await _users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<List<User>> SearchUsersByNameAsync(string name)
    {
        var users = await _usersRepository.SearchUsersByNameAsync(name);
        return _mapper.Map<List<User>>(users);
    }


    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _users.FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _users.ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
    
}