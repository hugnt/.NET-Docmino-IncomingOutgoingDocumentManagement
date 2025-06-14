using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Entities;
using System.Linq.Expressions;

public static class PositionMapping
{
    public static readonly Expression<Func<Position, PositionResponse>> SelectResponseExpression = x => new PositionResponse
    {
        Id = x.Id,
        Name = x.Name,
        Description = x.Description,
        DepartmentId = x.DepartmentId,
        DepartmentName = x.Department.Name
    };

    public static Position ToEntity(this PositionRequest request)
    {
        return new Position
        {
            Name = request.Name,
            Description = request.Description ?? "",
            DepartmentId = request.DepartmentId,
        };
    }

    public static void MappingFieldFrom(this Position entity, PositionRequest request)
    {
        entity.Name = request.Name;
        entity.Description = request.Description ?? "";
        entity.DepartmentId = request.DepartmentId;
    }
}