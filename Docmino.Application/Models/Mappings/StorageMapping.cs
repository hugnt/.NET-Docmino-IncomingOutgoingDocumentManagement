using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Entities;
using Docmino.Domain.Enums;
using System.Linq.Expressions;

namespace Docmino.Application.Models.Mappings;
public static class StorageMapping
{
    public static Storage ToEntity(this StorageRequest request)
    {
        return new Storage
        {
            Name = request.Name,
            Code = request.Code,
            Description = request.Description,
            DirectoryId = request.BoxId,
            Year = request.Year,
            StoragePeriodId = request.StoragePeriodId,
            Status = request.Status
        };
    }


    public static readonly Expression<Func<Storage, StorageResponse>> SelectResponseExpression = x => new StorageResponse
    {
        Id = x.Id,
        Name = x.Name,
        Code = x.Code,
        Description = x.Description ?? "",
        BoxId = x.DirectoryId,
        BoxName = x.Directory != null ? x.Directory.Name : "",
        Year = x.Year,
        StoragePeriodId = x.StoragePeriodId,
        StoragePeriodName = x.StoragePeriod != null ? x.StoragePeriod.Name : "",
        Status = x.Status,
        CreatedAt = x.CreatedAt,
        DocumentCount = x.Documents.Count()
    };

    public static readonly Expression<Func<Storage, StorageDetailResponse>> SelectDetailResponseExpression = x => new StorageDetailResponse
    {
        Id = x.Id,
        Name = x.Name,
        Code = x.Code,
        Description = x.Description ?? "",
        BoxId = x.DirectoryId,
        BoxName = x.Directory != null ? x.Directory.Name : "",
        Year = x.Year,
        StoragePeriodId = x.StoragePeriodId,
        StoragePeriodName = x.StoragePeriod != null ? x.StoragePeriod.Name : "",
        Status = x.Status,
        CreatedAt = x.CreatedAt,
        DocumentCount = x.Documents.Count(),
        Documents = x.Documents.Select(d => new PublishDocumentResponse
        {
            Id = d.Id,
            Name = d.Name,
            CodeNotation = d.CodeNotation ?? "",
            DocumentRegisterName = d.DocumentRegister != null ? d.DocumentRegister.Name : "",
            CategoryName = d.Category != null ? d.Category.Name : "",
            FieldName = d.Field != null ? d.Field.Name : "",
            DocumentType = d.DocumentRegister != null ? d.DocumentRegister.RegisterType : DocumentType.None,
            IssuedDate = d.IssuedDate,
            PublishDate = d.UpdatedAt,
        }).ToList()
    };

    public static void MappingFieldFrom(this Storage trackedEntity, StorageRequest request)
    {
        trackedEntity.Name = request.Name;
        trackedEntity.Code = request.Code;
        trackedEntity.Description = request.Description;
        trackedEntity.DirectoryId = request.BoxId;
        trackedEntity.Year = request.Year;
        trackedEntity.StoragePeriodId = request.StoragePeriodId;
        trackedEntity.Status = request.Status;

    }
}
