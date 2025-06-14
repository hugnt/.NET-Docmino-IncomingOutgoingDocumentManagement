using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Entities;
using Docmino.Domain.Enums;
using System.Linq.Expressions;

namespace Docmino.Application.Models.Mappings;
public static class InternalDocumentMapping
{
    public static Expression<Func<Document, InternalDocumentResponse>> SelectResponseExpression = x => new InternalDocumentResponse
    {
        Id = x.Id,
        CodeNotation = x.CodeNotation ?? string.Empty,
        Name = x.Name,
        CategoryName = x.Category.Name,
        DocumentRegisterName = x.DocumentRegister.Name,
        FieldName = x.Field.Name,
        IssuedDate = x.IssuedDate,
        Description = x.Description ?? string.Empty,
        DepartmentName = x.Department.Name,
        ArrivalNumber = x.ArrivalNumber ?? string.Empty,
        ArrivalDate = x.ArrivalDate,
        CodeNumber = x.CodeNumber ?? string.Empty,
        DocumentStatus = x.DocumentStatus,
    };

    public static Expression<Func<Document, InternalDocumentDetailResponse>> SelectDetailResponseExpression = x => new InternalDocumentDetailResponse
    {
        Id = x.Id,
        Name = x.Name,
        CategoryId = x.CategoryId,
        DocumentRegisterId = x.DocumentRegisterId,
        FieldId = x.FieldId,
        CodeNotation = x.CodeNotation ?? string.Empty,
        IssuedDate = x.IssuedDate,
        DepartmentId = x.DepartmentId ?? 0,
        Subject = x.Subject ?? string.Empty,
        PageAmount = x.PageAmount,
        Description = x.Description ?? string.Empty,
        SecurePriority = x.SecurePriority,
        UrgentPriority = x.UrgentPriority,
        ArrivalNumber = x.ArrivalNumber ?? string.Empty,
        ArrivalDate = x.ArrivalDate,
        ToPlaces = x.ToPlaces ?? string.Empty,
        CodeNumber = x.CodeNumber ?? string.Empty,
        DueDate = x.DueDate,
        IssuedAmount = x.IssuedAmount,
        DocumentStatus = x.DocumentStatus,
        DocumentFiles = x.DocumentFiles != null ? x.DocumentFiles.Select(f => new DocumentFileResponse
        {
            Id = f.Id,
            FileName = f.FileName,
            FileType = f.FileType ?? string.Empty,
            FileSize = f.FileSize,
            CreatedAt = f.CreatedAt,
            Description = f.Description ?? string.Empty,
        }).ToList() : null,
        ConfirmProcess = x.Process != null ? new ConfirmProcessResponse
        {
            Id = x.Process.Id,
            Name = x.Process.Name ?? string.Empty,
            Type = x.Process.Type,
            ManagerId = x.Process.ManagerId,
            BlockchainEnabled = x.Process.BlockchainEnabled,
            CurrentStepNumber = x.Process.CurrentStepNumber,
            Description = x.Process.Description ?? string.Empty,
            Status = x.Process.Status,
            ProcessDetails = null,
            ProcessHistories = null
        } : null
    };

    public static Document ToEntity(this InternalIncomingDocumentRequest documentRequest) => new()
    {
        Name = documentRequest.Name,
        ArrivalNumber = documentRequest.ArrivalNumber ?? string.Empty,
        ArrivalDate = documentRequest.ArrivalDate,
        CodeNotation = documentRequest.CodeNotation ?? string.Empty,
        CategoryId = documentRequest.CategoryId,
        DocumentRegisterId = documentRequest.DocumentRegisterId,
        FieldId = documentRequest.FieldId,
        DepartmentId = documentRequest.DepartmentId,
        Subject = documentRequest.Subject ?? string.Empty,
        ToPlaces = documentRequest.ToPlaces ?? string.Empty,
        Description = documentRequest.Description ?? string.Empty,
        IssuedDate = documentRequest.IssuedDate,
        PageAmount = documentRequest.PageAmount,
        IssuedAmount = documentRequest.IssuedAmount,
        SecurePriority = documentRequest.SecurePriority,
        UrgentPriority = documentRequest.UrgentPriority,
        DueDate = documentRequest.DueDate,
        DocumentStatus = documentRequest?.Status ?? DocumentStatus.Draff,
    };

    public static Document ToEntity(this InternalOutgoingDocumentRequest documentRequest) => new()
    {
        Name = documentRequest.Name,
        CodeNumber = documentRequest.CodeNumber!,
        CodeNotation = documentRequest.CodeNotation ?? string.Empty,
        CategoryId = documentRequest.CategoryId,
        DocumentRegisterId = documentRequest.DocumentRegisterId,
        FieldId = documentRequest.FieldId,
        DepartmentId = documentRequest.DepartmentId,
        Subject = documentRequest.Subject ?? string.Empty,
        ToPlaces = documentRequest.ToPlaces ?? string.Empty,
        Description = documentRequest.Description ?? string.Empty,
        IssuedDate = documentRequest.IssuedDate,
        PageAmount = documentRequest.PageAmount,
        IssuedAmount = documentRequest.IssuedAmount,
        SecurePriority = documentRequest.SecurePriority,
        UrgentPriority = documentRequest.UrgentPriority,
        DocumentStatus = documentRequest?.Status ?? DocumentStatus.Draff,
    };

    public static void MappingFieldFrom(this Document trackedEntity, InternalIncomingDocumentRequest request)
    {
        trackedEntity.Name = request.Name;
        trackedEntity.ArrivalNumber = request.ArrivalNumber ?? string.Empty;
        trackedEntity.ArrivalDate = request.ArrivalDate;
        trackedEntity.CodeNotation = request.CodeNotation ?? string.Empty;

        trackedEntity.DocumentRegisterId = request.DocumentRegisterId;
        trackedEntity.CategoryId = request.CategoryId;
        trackedEntity.FieldId = request.FieldId;
        trackedEntity.DepartmentId = request.DepartmentId;

        trackedEntity.Subject = request.Subject ?? string.Empty;
        trackedEntity.ToPlaces = request.ToPlaces ?? string.Empty;
        trackedEntity.Description = request.Description ?? string.Empty;

        trackedEntity.IssuedDate = request.IssuedDate;
        trackedEntity.PageAmount = request.PageAmount;
        trackedEntity.IssuedAmount = request.IssuedAmount;

        trackedEntity.DueDate = request.DueDate;
        trackedEntity.SecurePriority = request.SecurePriority;
        trackedEntity.UrgentPriority = request.UrgentPriority;

    }


    public static void MappingFieldFrom(this Document trackedEntity, InternalOutgoingDocumentRequest request)
    {
        trackedEntity.Name = request.Name;
        trackedEntity.ArrivalNumber = request.CodeNumber!;

        trackedEntity.CodeNotation = request.CodeNotation ?? string.Empty;

        trackedEntity.DocumentRegisterId = request.DocumentRegisterId;
        trackedEntity.CategoryId = request.CategoryId;
        trackedEntity.FieldId = request.FieldId;
        trackedEntity.DepartmentId = request.DepartmentId;

        trackedEntity.Subject = request.Subject ?? string.Empty;
        trackedEntity.ToPlaces = request.ToPlaces ?? string.Empty;
        trackedEntity.Description = request.Description ?? string.Empty;

        trackedEntity.IssuedDate = request.IssuedDate;
        trackedEntity.PageAmount = request.PageAmount;
        trackedEntity.IssuedAmount = request.IssuedAmount;

        trackedEntity.SecurePriority = request.SecurePriority;
        trackedEntity.UrgentPriority = request.UrgentPriority;

    }
}
