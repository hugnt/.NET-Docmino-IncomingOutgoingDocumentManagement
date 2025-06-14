using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Entities;
using System.Linq.Expressions;

namespace Docmino.Application.Models.Mappings;
public static class DocumentDirectoryMapping
{
    public static DocumentDirectory ToEntity(this DocumentDirectoryRequest request)
    {
        return new DocumentDirectory
        {
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            ParentDirectoryId = request.ParentDirectoryId,
        };
    }

    public static readonly Expression<Func<DocumentDirectory, DirectoryTreeItemResponse>> SelectTreeItemResponseExpression = x => new DirectoryTreeItemResponse
    {
        Id = x.Id,
        Name = x.Name,
        ParentDirectoryId = x.ParentDirectoryId,
        Type = x.Type,
        DocumentCount = x.Storages != null ? x.Storages.SelectMany(s => s.Documents).Count() : 0
    };

    public static readonly Expression<Func<DocumentDirectory, DocumentDirectoryResponse>> SelectResponseExpression = x => new DocumentDirectoryResponse
    {
        Id = x.Id,
        Name = x.Name,
        Type = x.Type,
        Description = x.Description ?? "",
        ParentDirectoryId = x.ParentDirectoryId,
        ParentDirectoryName = x.ParentDirectory != null ? x.ParentDirectory.Name : null,
        CreatedAt = x.CreatedAt,
    };

    public static void MappingFieldFrom(this DocumentDirectory trackedEntity, DocumentDirectoryRequest request)
    {
        trackedEntity.Name = request.Name;
        trackedEntity.Description = request.Description;
        trackedEntity.Type = request.Type;
        trackedEntity.ParentDirectoryId = request.ParentDirectoryId;
    }
}
