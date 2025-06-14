using Docmino.Domain.Entities;
using System.Linq.Expressions;

public static class DocumentFieldMapping
{
    public static DocumentField ToEntity(this DocumentFieldRequest request)
    {
        return new DocumentField
        {
            Name = request.Name.Trim(),
            Code = request.Code.Trim(),
            Description = request.Description?.Trim() ?? string.Empty
        };
    }

    public static void MappingFieldFrom(this DocumentField entity, DocumentFieldRequest request)
    {
        entity.Name = request.Name.Trim();
        entity.Code = request.Code.Trim();
        entity.Description = request.Description?.Trim() ?? string.Empty;
    }

    public static Expression<Func<DocumentField, DocumentFieldResponse>> SelectResponseExpression =>
        x => new DocumentFieldResponse
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            Description = x.Description
        };
}