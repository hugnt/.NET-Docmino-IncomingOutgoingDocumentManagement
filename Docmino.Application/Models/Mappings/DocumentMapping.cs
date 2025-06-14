using Docmino.Application.Models.Responses;
using Docmino.Domain.Entities;
using Docmino.Domain.Enums;
using System.Linq.Expressions;

namespace Docmino.Application.Models.Mappings;
public static class DocumentMapping
{
    public static Expression<Func<Document, DocumentDetailResponse>> SelectDetailProcessingDocumentResponseExpression = x => new DocumentDetailResponse
    {
        Id = x.Id,
        Name = x.Name,
        CategoryId = x.CategoryId,
        DocumentRegisterId = x.DocumentRegisterId,
        FieldId = x.FieldId,
        CodeNotation = x.CodeNotation ?? string.Empty,
        CodeNumber = x.CodeNumber ?? string.Empty,
        IssuedDate = x.IssuedDate,
        OrganizationId = x.OrganizationId ?? 0,
        Subject = x.Subject ?? string.Empty,
        PageAmount = x.PageAmount,
        Description = x.Description ?? string.Empty,
        SecurePriority = x.SecurePriority,
        UrgentPriority = x.UrgentPriority,
        ArrivalNumber = x.ArrivalNumber ?? string.Empty,
        ArrivalDate = x.ArrivalDate,
        ToPlaces = x.ToPlaces ?? string.Empty,
        DueDate = x.DueDate,
        IssuedAmount = x.IssuedAmount,
        DocumentStatus = x.DocumentStatus,
        DocumentFiles = x.DocumentFiles != null ? x.DocumentFiles.Select(f => new DocumentFileResponse
        {
            Id = f.Id,
            FileName = f.FileName,
            FileType = f.FileType ?? string.Empty,
            FileUrl = f.FileUrl,
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
            ProcessDetails = x.Process.ProcessDetails != null ? x.Process.ProcessDetails.Select(pd => new ProcessDetailResponse
            {
                Id = pd.Id,
                StepNumber = pd.StepNumber,
                ReviewerType = pd.ReviewerType,
                SignType = pd.SignType,
                ReviewerUserId = pd.ReviewerUserId,
                ReviewerGroupId = pd.ReviewerGroupId,
                ReviewerPositionId = pd.ReviewerPositionId,
                ReviewerDepartmentId = pd.ReviewerDepartmentId,
                ReviewerName = pd.ReviewerName ?? string.Empty,
                ActionName = pd.ActionName ?? string.Empty,
                VetoRight = pd.VetoRight,
                DateStart = pd.DateStart,
                DateEnd = pd.DateEnd,
                ResignDateEnd = pd.ResignDateEnd
            }).OrderBy(x => x.StepNumber).ToList() : null,
            ProcessHistories = x.Process.ProcessHistories != null ? x.Process.ProcessHistories.Select(ph => new ProcessHistoryResponse
            {
                Id = ph.Id,
                ProcessId = ph.ProcessId,
                ProcessName = ph.ProcessName ?? string.Empty,
                CurrentStepNumber = ph.CurrentStepNumber,
                CurrentStatusName = ph.CurrentStatusName ?? string.Empty,
                ReviewerName = ph.ReviewerName ?? string.Empty,
                UserReviewerName = ph.UserReviewer.Fullname ?? string.Empty,
                Comment = ph.Comment ?? string.Empty,
                NextStepNumber = ph.NextStepNumber,
                ActionName = ph.ActionName ?? string.Empty,
                TxHash = ph.TxHash ?? string.Empty,
                CreatedAt = ph.CreatedAt,
                ProcessSignHistories = ph.ProcessSignHistories.Select(z => z.FileUrl).ToList()
            }).ToList() : null
        } : null
    };

    public static Expression<Func<Document, PublishDocumentResponse>> SelectPublishDocumentResponseExpression = x => new PublishDocumentResponse
    {
        Id = x.Id,
        Name = x.Name,
        CodeNotation = x.CodeNotation ?? "",
        DocumentRegisterName = x.DocumentRegister != null ? x.DocumentRegister.Name : "",
        CategoryName = x.Category != null ? x.Category.Name : "",
        FieldName = x.Field != null ? x.Field.Name : "",
        DocumentType = x.DocumentRegister != null ? x.DocumentRegister.RegisterType : DocumentType.None,
        IssuedDate = x.IssuedDate,
        PublishDate = x.UpdatedAt,
    };
}
