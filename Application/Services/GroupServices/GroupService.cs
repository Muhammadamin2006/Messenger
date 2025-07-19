using AutoMapper;
using Messenger.Application.Dtos.GroupDto;
using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;
using Messenger.Infrastracture.Repositories.GroupRepositories;
using Microsoft.AspNetCore.Http.HttpResults;
using StackExchange.Redis;

namespace Messenger.Application.Services.GroupServices;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGroupUserRepository _groupUserRepository;
    private readonly IMapper _mapper;
    private readonly MessengerContext _context;

    public GroupService(IGroupRepository groupRepository, IUserRepository userRepository, IGroupUserRepository groupUserRepository,  IMapper mapper, 
        MessengerContext context)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
        _groupUserRepository = groupUserRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<GroupDto> CreateGroupAsync(CreateGroupDto dto, string creatorEmail, Guid userId)
    {
        var creator = await _userRepository.GetByEmailAsync(creatorEmail);
            if(creator == null)
                throw new Exception($"User with email {creatorEmail} not found");
            
        var groupExists = await _groupRepository.IsGroupNameTakenByUserAsync(dto.GroupName, userId);
        if (string.IsNullOrWhiteSpace(dto.GroupName))
            throw new Exception("Group name cannot be empty");
        
        if (groupExists)
            throw new Exception("Group with this name already exists");

        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            GroupName = dto.GroupName,
            GroupCreatedAt = DateTime.UtcNow,
            GroupCreatedByUserId = creator.UserId,
            GroupUsers = new List<GroupUser>()
        };

        var groupUser = new GroupUser
        {
            GroupUserId = Guid.NewGuid(),
            GroupId = group.GroupId,
            UserId = creator.UserId,
            Status = "Admin"
        };

        group.GroupUsers.Add(groupUser);

        await _groupRepository.AddAsync(group);
        await _context.SaveChangesAsync();

        return _mapper.Map<GroupDto>(group);
    }


    public async Task<List<GroupUsersWithStatusDto>> GetGroupUsersWithStatusAsync(Guid groupId)
    {
        var group = await _groupRepository.GetGroupWithUsersAsync(groupId);
        if (group == null)
            throw new Exception("Group not found");

        return group.GroupUsers.Select(gu => new GroupUsersWithStatusDto
        {
            GroupUserId = gu.UserId,
            GroupUserName = gu.User.Username,
            GroupUserEmail = gu.User.Email,
            GroupUserStatus = gu.Status,
        }).ToList();
    }
    

    public async Task<GroupDto?> GetGroupByIdAsync(Guid groupId)
    {
        var group = await _groupRepository.GetGroupChatByIdAsync(groupId);
        if(group == null)
            return null;
        return _mapper.Map<GroupDto>(group);
    }


    public async Task DeleteGroupAsync(Guid groupId, Guid initiatorId)
    {
        var group = await _groupRepository.GetByIdAsync(groupId)
                    ?? throw new Exception($"The group by id: {groupId} not found");

        if (group.GroupCreatedByUserId != initiatorId)
            throw new Exception("Only admin can delete this group");

        await _groupRepository.DeleteGroupChatCompletelyAsync(groupId);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AssignNewAdminAsync(Guid groupId, Guid newAdminUserId, string requesterEmail)
    {
        var group = await _groupRepository.GetGroupWithUsersAsync(groupId)
                    ?? throw new Exception($"The group by id: {groupId} not found");

        var requester = await _userRepository.GetByEmailAsync(requesterEmail)
                        ?? throw new Exception($"The user by Email: {requesterEmail} not found");

        if (group.GroupCreatedByUserId != requester.UserId)
            throw new Exception("Only current admin can give his admin status ");

        if (group.IsAdmin)
            throw new Exception("This member already has admin status");
        var target = group.GroupUsers.FirstOrDefault(u => u.UserId == newAdminUserId)
                     ?? throw new Exception($"The user by id: {newAdminUserId} don't exist in group");

        target.IsAdmin = true;
        _groupRepository.Update(group);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task RenameGroupAsync(Guid groupId, string newName, Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");
        
        var groupUser = await _groupUserRepository.GetByUserAndGroupAsync(userId, groupId);
        if (groupUser == null)
            throw new Exception("Вы не являетесь участником группы.");

        if (groupUser.Status != "Admin")
            throw new Exception("Only admin can rename this group");

        var group = await _groupRepository.GetByIdAsync(groupId);
        if (group == null)
            throw new Exception("Group not found.");

        var isNameUsed = await _groupRepository.IsGroupNameTakenForUserAsync(newName, groupUser.User.Email);
        if (isNameUsed)
            throw new Exception("You already have a group with that name");

        group.GroupName = newName;
        _groupRepository.Update(group);
    }

}

    
    
    


    

