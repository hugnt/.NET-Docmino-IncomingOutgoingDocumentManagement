using Docmino.Domain.Entities;
using System.Linq.Expressions;

public static class DocumentRegisterMapping
{
    public static DocumentRegister ToEntity(this DocumentRegisterRequest request)
    {
        return new DocumentRegister
        {
            Name = request.Name.Trim(),
            Year = request.Year,
            IsActive = request.IsActive,
            Description = request.Description?.Trim() ?? "",
            RegisterType = request.RegisterType
        };
    }

    public static void MappingFieldFrom(this DocumentRegister entity, DocumentRegisterRequest request)
    {
        entity.Name = request.Name.Trim();
        entity.Year = request.Year;
        entity.IsActive = request.IsActive;
        entity.Description = request.Description?.Trim() ?? "";
        entity.RegisterType = request.RegisterType;
    }

    public static Expression<Func<DocumentRegister, DocumentRegisterResponse>> SelectResponseExpression =>
        x => new DocumentRegisterResponse
        {
            Id = x.Id,
            Name = x.Name,
            Year = x.Year,
            IsActive = x.IsActive,
            Description = x.Description,
            RegisterType = x.RegisterType,
        };
}