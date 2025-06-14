using Docmino.Application.Common.Messages;
using Docmino.Application.Helpers;
using Docmino.Application.Models;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;
using System.Net;

public class GroupService : IGroupService
{
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<UserGroup> _userGroupRepository;
    private readonly IValidator<GroupRequest> _validator;

    public GroupService(IRepository<Group> repository,
        IValidator<GroupRequest> validator,
        IRepository<UserGroup> userGroupRepository)
    {
        _groupRepository = repository;
        _validator = validator;
        _userGroupRepository = userGroupRepository;
    }

    public async Task<Result> GetAll(FilterRequest filter)
    {
        Expression<Func<Group, bool>> queryFilter = x =>
            filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!);

        var res = await _groupRepository.GetByFilterAsync(filter.PageSize, filter.PageNumber, predicate: queryFilter, selectQuery: GroupMapping.SelectResponseExpression);
        return FilterResult<List<GroupResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }

    public async Task<Result> GetById(Guid id)
    {
        var selectedEntity = await _groupRepository.FirstOrDefaultAsync(x => x.Id == id, selectQuery: GroupMapping.SelectResponseExpression);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy nhóm với Id {id}");
        }
        return Result<GroupResponse>.SuccessWithBody(selectedEntity);
    }

    public async Task<Result> Add(GroupRequest modelRequest)
    {
        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _groupRepository.AnyAsync(x => x.Name == modelRequest.Name.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, $"Nhóm '{modelRequest.Name}' đã tồn tại.");
        }

        var newEntity = modelRequest.ToEntity();
        _groupRepository.Add(newEntity);

        if (modelRequest.Members != null && modelRequest.Members.Any())
        {
            var userGroups = modelRequest.Members.Select(user => new UserGroup
            {
                UserId = user.UserId,
                GroupId = newEntity.Id,
                GroupRole = user.GroupRole
            }).ToList();
            await _userGroupRepository.AddRangeAsync(userGroups);
        }

        await _groupRepository.SaveChangesAsync();
        return Result.Success(HttpStatusCode.Created, "Tạo nhóm thành công.");
    }

    public async Task<Result> Update(Guid id, GroupRequest modelRequest)
    {
        var selectedEntity = await _groupRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy nhóm với Id {id}");
        }

        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _groupRepository.AnyAsync(x => x.Id != id && x.Name == modelRequest.Name.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, $"Nhóm '{modelRequest.Name}' đã tồn tại.");
        }

        var currentUserGroup = await _userGroupRepository.GetAllAsync(x => x.GroupId == id);
        var currentMemberIds = currentUserGroup.Select(x => x.UserId).ToList();
        var newMemberIds = modelRequest.Members != null ? modelRequest.Members.Select(x => x.UserId) : new List<Guid>();
        var membersToAdd = newMemberIds.Except(currentMemberIds).ToList();
        var membersToRemove = currentMemberIds.Except(newMemberIds).ToList();

        foreach (var user in currentUserGroup)
        {
            if (modelRequest.Members != null && modelRequest.Members.Any(x => x.UserId == user.UserId))
            {
                var updatedRole = modelRequest.Members.First(x => x.UserId == user.UserId).GroupRole;
                if (user.GroupRole != updatedRole)
                {
                    user.GroupRole = updatedRole;
                    _userGroupRepository.Update(user);
                }
            }
        }
        if (membersToAdd.Any())
        {
            var userGroupsToAdd = membersToAdd.Select(userId => new UserGroup
            {
                UserId = userId,
                GroupRole = modelRequest.Members!.First(x => x.UserId == userId).GroupRole,
                GroupId = id
            }).ToList();
            await _userGroupRepository.AddRangeAsync(userGroupsToAdd);
        }
        if (membersToRemove.Any())
        {
            var userGroupsToRemove = currentUserGroup.Where(modelRequest => membersToRemove.Contains(modelRequest.UserId)).ToList();
            _userGroupRepository.DeleteRange(userGroupsToRemove);
        }

        selectedEntity.MappingFieldFrom(modelRequest);
        _groupRepository.Update(selectedEntity);
        await _groupRepository.SaveChangesAsync();

        return Result.SuccessNoContent();
    }

    public async Task<Result> Delete(Guid id)
    {
        var selectedEntity = await _groupRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy nhóm với Id {id}");
        }
        if (await _groupRepository.AnyAsync(x => x.ProcessDetails != null))
        {
            return Result.ErrorWithMessage(ErrorMessage.ObjectIsInOtherProcess("Nhóm", selectedEntity.Name));
        }
        _groupRepository.Delete(selectedEntity);
        await _groupRepository.SaveChangesAsync();
        return Result.SuccessNoContent();
    }
}