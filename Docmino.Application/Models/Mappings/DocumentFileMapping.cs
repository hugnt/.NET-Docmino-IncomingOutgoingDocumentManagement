using Docmino.Application.Models.Requests;
using Docmino.Domain.Entities;

namespace Docmino.Application.Models.Mappings;
public static class DocumentFileMapping
{
    public static DocumentFile ToEntity(this DocumentFileRequest fileRequest, Guid documentId) => new DocumentFile()
    {
        DocumentId = documentId,
        FileName = fileRequest.FileName,
        FileType = fileRequest.FileType,
        FileUrl = fileRequest.FileUrl ?? "",
        FileSize = fileRequest.FileSize ?? 0,
        FileEncoding = fileRequest.FileEncoding,
        Description = fileRequest.Description
    };

    public static void MappingFieldFrom(this DocumentFile trackedEntity, DocumentFileRequest request)
    {
        trackedEntity.FileName = request.FileName;
        trackedEntity.FileType = request.FileType;
        trackedEntity.FileSize = request.FileSize ?? 0;
        trackedEntity.FileEncoding = request.FileEncoding;
        trackedEntity.Description = request.Description;
    }
}
