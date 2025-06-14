using Docmino.Application.Models.Requests;
using Docmino.Domain.Entities;
using System.Linq.Expressions;

public static class DepartmentMapping
{
    public static readonly Expression<Func<Department, DepartmentResponse>> SelectResponseExpression = x => new DepartmentResponse
    {
        Id = x.Id,
        Name = x.Name,
        Code = x.Code ?? "",
        Description = x.Description ?? "",
        ParentDepartmentId = x.Department0 != null ? x.Department0.Id : (int?)null,
        ParentDepartmentName = x.Department0 != null ? x.Department0.Name : null
    };

    public static Department ToEntity(this DepartmentRequest request)
    {
        return new Department
        {
            Name = request.Name,
            Code = request.Code,
            Description = request.Description,
            Id0 = request.ParentDepartmentId
        };
    }

    public static void MappingFieldFrom(this Department entity, DepartmentRequest request)
    {
        entity.Name = request.Name;
        entity.Code = request.Code;
        entity.Description = request.Description;
        entity.Id0 = request.ParentDepartmentId;

    }
}