using Docmino.Domain.Entities;
using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Responses;
public class ConfirmProcessResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CurrentStepNumber { get; set; }
    public ProcessType Type { get; set; }
    public Guid ManagerId { get; set; }
    public bool BlockchainEnabled { get; set; }
    public string Description { get; set; }
    public ProcessStatus Status { get; set; }

    public List<ProcessDetailResponse>? ProcessDetails { get; set; }

    public List<ProcessHistoryResponse>? ProcessHistories { get; set; }
}

public class ProcessDetailResponse
{
    public Guid Id { get; set; }
    public int StepNumber { get; set; }
    public SignType SignType { get; set; }
    public ReviewerType ReviewerType { get; set; }
    public Guid? ReviewerUserId { get; set; }
    public Guid? ReviewerGroupId { get; set; }
    public int? ReviewerPositionId { get; set; }
    public int? ReviewerDepartmentId { get; set; }
    public string? ReviewerName { get; set; }
    public string? ActionName { get; set; }
    public bool VetoRight { get; set; }
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public DateOnly? ResignDateEnd { get; set; }
    public List<ProcessSignDetailResponse>? SignDetails { get; set; }
}

public class ProcessHistoryResponse
{
    public Guid Id { get; set; }
    public Guid ProcessId { get; set; }
    public string? ProcessName { get; set; }
    public int CurrentStepNumber { get; set; }
    public string? CurrentStatusName { get; set; }
    public string ReviewerName { get; set; }
    public string UserReviewerName { get; set; }
    public string? Comment { get; set; }
    public int NextStepNumber { get; set; }
    public string? ActionName { get; set; }
    public string? TxHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<string> ProcessSignHistories { get; set; }
}

public class ProcessSignDetailResponse
{
    public Guid Id { get; set; }
    public Guid FileId { get; set; }
    public double PosX { get; set; }
    public double PosY { get; set; }
    public double SignZoneWidth { get; set; }
    public double SignZoneHeight { get; set; }
    public int SignPage { get; set; }
    public double TranslateX { get; set; }
    public double TranslateY { get; set; }

    public string FileUrl { get; set; }
    public string FileName { get; set; }
}

public class DocumentProcessDetailResponse
{
    public string DocumentName { get; set; }
    public string CodeNotation { get; set; }
    public DocumentType DocumentType { get; set; }
    public UrgentPriority UrgentPriority { get; set; }

    public string? ReviewerName { get; set; }
    public string? ActionName { get; set; }
    public int StepNumber { get; set; }
    public SignType SignType { get; set; }
    public ReviewerType ReviewerType { get; set; }
    public User? ReviewerUser { get; set; }
    public List<User>? ReviewerGroupUser { get; set; }
    public List<User>? ReviewerPositionUser { get; set; }
    public List<User>? ReviewerDepartmentUser { get; set; }
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
}
