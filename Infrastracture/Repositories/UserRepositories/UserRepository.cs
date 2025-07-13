using Messenger.Application.Dtos;
using Messenger.Application.Services;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly MessengerContext _context;
    private readonly DbSet<User> _users;
    
    public UserRepository(MessengerContext context) : base(context)
    {
        _context = context;
        _users = context.Set<User>();
        
    }


    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task DeleteAsync(User user)
    {
        if (_context.Entry(user).State == EntityState.Detached)
            _users.Attach(user);

         _users.Remove(user);
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        if (user.Id == Guid.Empty)
            user.Id = Guid.NewGuid(); 

        await _users.AddAsync(user);
        await _context.SaveChangesAsync();
    }


    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await _users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        
    }
}