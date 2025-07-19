using AutoMapper;
using Messenger.Application.Dtos.GroupDto;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;
using Messenger.Infrastracture.Repositories.GroupRepositories;

namespace Messenger.Application.Services.GroupServices;

public class GroupUserService : IGroupUserService
{
    private readonly IGroupUserRepository _groupUserRepository;
    private readonly IMapper _mapper;
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    private readonly MessengerContext _context;

    public GroupUserService(IGroupUserRepository groupUserRepository, IMapper mapper, MessengerContext  context, IUserRepository userRepository,
        IGroupRepository groupRepository)
    {
        _groupUserRepository = groupUserRepository;
        _mapper = mapper;
        _context = context;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
    }

    public async Task AddUserToGroupAsync(Guid groupId, Guid userId, Guid addedByUserId, string status = "Member")
    {
        var group = await _groupRepository.GetByIdAsync(groupId) 
                    ?? throw new Exception($"The group by id: {groupId} not found");

        var user = await _userRepository.GetByIdAsync(userId) 
                   ?? throw new Exception($"The user by id: {userId} not found");

        var addedBy = await _groupUserRepository.GetByUserAndGroupAsync(addedByUserId, groupId) 
                      ?? throw new Exception($"The user by id: {userId} don't exist in group");

        if (await _groupUserRepository.GetByUserAndGroupAsync(userId, groupId) != null)
            throw new Exception("You already exist in group");

        var groupUser = new GroupUser
        {
            GroupUserId = Guid.NewGuid(),
            GroupId = groupId,
            UserId = userId,
            Status = status
        };

        await _groupUserRepository.AddAsync(groupUser);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task LeaveGroupAsync(Guid groupId, string userEmail)
    {
        var user = await _userRepository.GetByEmailAsync(userEmail)
                   ?? throw new Exception($"The user not found");

        var groupUser = await _groupUserRepository.GetByUserAndGroupAsync(user.UserId, groupId)
                        ?? throw new Exception("You are not exist in group");

        _groupUserRepository.Delete(groupUser);

        var remainingUsers = await _groupUserRepository.GetUsersInGroupChatAsync(groupId);

        if (remainingUsers.Count == 0)
        {
            await _groupRepository.DeleteGroupChatCompletelyAsync(groupId);
        }

        await _context.SaveChangesAsync();
        
    }
    
    public async Task AppointUserToAdminAsync(AppointToAdminDto dto)
    {
        var group = await _groupRepository.GetGroupWithUsersAsync(dto.GroupId)
                    ?? throw new Exception($"Group not found");

        var requester = group.GroupUsers.FirstOrDefault(u => u.User.Email == dto.RequesterEmail);
        if (requester == null || requester.Status != "Admin")
            throw new Exception("Only admin can appoint user his status");

        var target = group.GroupUsers.FirstOrDefault(u => u.UserId == dto.TargetUserId)
                     ?? throw new Exception($"User not found in group");

        if (target.IsAdmin)
            throw new Exception("User is already an admin");

        target.IsAdmin = true;
        _groupUserRepository.Update(target);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task RemoveUserFromGroupByIdAsync(Guid groupId, Guid adminId, Guid targetUserId)
    {
        var group = await _groupRepository.GetGroupWithUsersAsync(groupId)
                    ?? throw new Exception($"The group by id: {groupId} not found");

        var adminUser = group.GroupUsers.FirstOrDefault(gu => gu.UserId == adminId);
        if (adminUser == null || !adminUser.IsAdmin)
            throw new Exception("Only admin can remove members");
        
        if (adminId == targetUserId)
            throw new Exception("You can't remove yourself");

        var targetUser = group.GroupUsers.FirstOrDefault(gu => gu.UserId == targetUserId);
        if (targetUser == null)
            throw new Exception("User not found in this group");
        group.GroupUsers.Remove(targetUser);

        var remainingUsers = group.GroupUsers.Where(gu => gu.UserId != targetUserId).ToList();

        if (remainingUsers.Count <= 1)
        {
            await _groupRepository.DeleteGroupChatCompletelyAsync(groupId);
        }

        await _context.SaveChangesAsync();
    }
    
    
    
}