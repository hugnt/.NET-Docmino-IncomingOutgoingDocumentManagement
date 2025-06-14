using Docmino.Application.Models.Responses;
using Docmino.Domain.Entities;
using System.Linq.Expressions;

namespace Docmino.Application.Models.Mappings;
public static class ProcessHistoryMapping
{
    public static ProcessHistory ToProcessHistoryEntity(this ProcessDetailResponse request, Guid confirmProcessId) => new()
    {
        ProcessId = confirmProcessId,
        CurrentStepNumber = request.StepNumber,
        ReviewerName = request.ReviewerName ?? string.Empty,
        ActionName = request.ActionName ?? string.Empty,
        CreatedAt = DateTime.Now,

    };

    public static Expression<Func<ProcessHistory, ProcessHistoryResponse>> SelectProcessHistoryResponseExpression = x => new ProcessHistoryResponse
    {
        Id = x.Id,
        ProcessId = x.ProcessId,
        ProcessName = x.ProcessName ?? string.Empty,
        CurrentStepNumber = x.CurrentStepNumber,
        CurrentStatusName = x.CurrentStatusName ?? string.Empty,
        ReviewerName = x.ReviewerName ?? string.Empty,
        UserReviewerName = x.UserReviewer.Fullname ?? string.Empty,
        Comment = x.Comment ?? string.Empty,
        NextStepNumber = x.NextStepNumber,
        ActionName = x.ActionName ?? string.Empty,
        TxHash = x.TxHash ?? string.Empty,
        CreatedAt = x.CreatedAt,
        ProcessSignHistories = x.ProcessSignHistories.Select(z => z.FileUrl).ToList()
    };
}
