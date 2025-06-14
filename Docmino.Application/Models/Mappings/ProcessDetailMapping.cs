using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Entities;
using System.Linq.Expressions;

namespace Docmino.Application.Models.Mappings;
public static class ProcessDetailMapping
{
    public static ProcessDetail ToEntity(this ProcessDetailRequest request, Guid processId) => new ProcessDetail()
    {
        ProcessId = processId,
        StepNumber = request.StepNumber,
        ReviewerType = request.ReviewerType,
        SignType = request.SignType,
        VetoRight = request.VetoRight,
        DateStart = request.DateStart,
        ActionName = request.ActionName,
        ReviewerName = request.ReviewerName,
        DateEnd = request.DateEnd ?? request.DateStart.AddDays(10),
        ResignDateEnd = request.ResignDateEnd
    };

    public static ProcessSignDetail ToEntity(this ProcessSignDetailRequest request, Guid processDetailsId, Guid fileId) => new ProcessSignDetail()
    {
        FileId = fileId,
        ProcessDetailsId = processDetailsId,
        PosX = request.PosX,
        PosY = request.PosY,
        SignZoneWidth = request.SignZoneWidth,
        SignZoneHeight = request.SignZoneHeight,
        SignPage = request.SignPage ?? 0,
        TranslateX = request.TranslateX ?? 0,
        TranslateY = request.TranslateY ?? 0
    };


    public static Expression<Func<ProcessDetail, ProcessingDocumentResponse>> SelectDocumentResponseExpression = x => new ProcessingDocumentResponse
    {
        Id = x.Process.DocumentId,
        Name = x.Process.Document.Name,
        DocumentRegisterName = x.Process.Document.DocumentRegister.Name,
        DocumentType = x.Process.Document.DocumentRegister.RegisterType,
        CurrentStepNumber = x.Process.CurrentStepNumber,
        CodeNotation = x.Process.Document.CodeNotation ?? string.Empty,
        CategoryName = x.Process.Document.Category.Name,
        IssuedDate = x.Process.Document.IssuedDate,
        TotalStepNumber = x.Process.ProcessDetails.Count
    };

    public static Expression<Func<ProcessDetail, ProcessDetailResponse>> SelectProcessDetailResponseExpression = x => new ProcessDetailResponse
    {
        Id = x.Id,
        StepNumber = x.StepNumber,
        ReviewerType = x.ReviewerType,
        SignType = x.SignType,
        ReviewerUserId = x.ReviewerUserId,
        ReviewerGroupId = x.ReviewerGroupId,
        ReviewerPositionId = x.ReviewerPositionId,
        ReviewerDepartmentId = x.ReviewerDepartmentId,
        ReviewerName = x.ReviewerName ?? string.Empty,
        ActionName = x.ActionName ?? string.Empty,
        VetoRight = x.VetoRight,
        DateStart = x.DateStart,
        DateEnd = x.DateEnd,
        ResignDateEnd = x.ResignDateEnd,
        SignDetails = x.ProcessSignDetails != null ? x.ProcessSignDetails.Select(sd => new ProcessSignDetailResponse
        {
            Id = sd.Id,
            FileId = sd.FileId,
            FileUrl = sd.File.FileUrl,
            FileName = sd.File.FileName,
            PosX = sd.PosX,
            PosY = sd.PosY,
            SignZoneWidth = sd.SignZoneWidth,
            SignZoneHeight = sd.SignZoneHeight,
            SignPage = sd.SignPage,
            TranslateX = sd.TranslateX,
            TranslateY = sd.TranslateY
        }).ToList() : new List<ProcessSignDetailResponse>()
    };

    public static Expression<Func<ProcessDetail, DocumentProcessDetailResponse>> SelectDocumentProcessDetailResponseExpression = x => new DocumentProcessDetailResponse
    {
        DocumentName = x.Process.Document.Name,
        CodeNotation = x.Process.Document.CodeNotation ?? string.Empty,
        DocumentType = x.Process.Document.DocumentRegister.RegisterType,
        ReviewerName = x.ReviewerName ?? string.Empty,
        ActionName = x.ActionName ?? string.Empty,
        StepNumber = x.StepNumber,
        SignType = x.SignType,
        ReviewerType = x.ReviewerType,
        ReviewerUser = x.ReviewerUser,
        ReviewerGroupUser = x.ReviewerGroup != null ? x.ReviewerGroup.UserGroups.Select(y => y.User).ToList() : null,
        ReviewerPositionUser = x.ReviewerPosition != null ? x.ReviewerPosition.Users.ToList() : null,
        ReviewerDepartmentUser = x.ReviewerDepartment != null ? (
                                        x.ReviewerDepartment.Positions != null ?
                                        x.ReviewerDepartment.Positions.SelectMany(y => y.Users).Distinct().ToList()
                                        : null
                                ) : null,
        DateStart = x.DateStart,
        DateEnd = x.DateEnd
    };
}
