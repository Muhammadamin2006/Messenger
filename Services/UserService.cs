using AutoMapper;
using Messenger.Infrastractures.Database;
using Messenger.DTOs;
using Messenger.Models;

namespace Messenger.Services;

public class UserService : IUserService 
{
    private readonly MessengerContext _context;
    private readonly IMapper _mapper;
    private readonly UserDto _userDto;
    private readonly User _user;


    public UserService(MessengerContext context, IMapper iMapper)
    {
        _context = context;
        _mapper = iMapper;
    }
        
    

    
    public IQueryable GetAll(UserDto userDto)
    {
        var user2 = new User()
        {
            UserId = userDto.UserId,
            Username = userDto.UserName,
        };

        var getall =  _context.Set<User>();
        return _mapper.Map<IQueryable<User>, IQueryable<UserDto>>(getall);

    }

    
    
    public UserDto GetById(Guid id)
    {
        if (_userDto.UserId == id)
        {
            var userr = new User()
            {
                UserId = _userDto.UserId,
                Username = _userDto.UserName,
            };
        } 
        var getbyid = _context.Users.Find(id);
        return _mapper.Map<UserDto>(getbyid);
    }

    public bool TryUpdate(UpdateUserDto updateUserDto)
    {
        _context.Users.Find(updateUserDto.UserId); 
        if (_user ==  null) return false;
        
        else
        {
            _user.Username = updateUserDto.UserName;
        }

        _context.Users.Update(_user);
        return _context.SaveChanges() > 0;
    }

    public bool Add(CreateUserDto createUserDto)
    {
        var uuser = _mapper.Map<User>(createUserDto);
        _context.Users.Add(uuser);
        return _context.SaveChanges() > 0;
        
    }

    public bool TryDelete(Guid userid)
    {
        var user = _context.Users.Find(userid);
        if (user == null) return false;

        _context.Users.Remove(user);
        return _context.SaveChanges() > 0;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    
}

