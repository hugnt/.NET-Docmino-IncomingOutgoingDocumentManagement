using Docmino.Domain.Entities;
using System.Linq.Expressions;

public static class GroupMapping
{
    public static Group ToEntity(this GroupRequest request)
    {
        return new Group
        {
            Name = request.Name.Trim()
        };
    }

    public static void MappingFieldFrom(this Group entity, GroupRequest request)
    {
        entity.Name = request.Name.Trim();

    }

    public static Expression<Func<Group, GroupResponse>> SelectResponseExpression => x => new GroupResponse
    {
        Id = x.Id,
        Name = x.Name,
        Members = x.UserGroups.Select(ug => new MemberInfoResponse
        {
            UserId = ug.User.Id,
            Fullname = ug.User.Fullname,
            GroupRole = ug.GroupRole,
            DepartmentName = ug.User.Position.Department.Name
        }).ToList()
    };
}