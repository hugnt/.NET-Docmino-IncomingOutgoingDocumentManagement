using Docmino.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Docmino.Application.Models.Requests;

public class ProcessDetailRequest
{
    public Guid? Id { get; set; }
    public int StepNumber { get; set; }
    public ReviewerType ReviewerType { get; set; }
    public Guid? ReviewerUserId { get; set; }
    public Guid? ReviewerGroupId { get; set; }
    public int? ReviewerPositionId { get; set; }
    public int? ReviewerDepartmentId { get; set; }
    public string? ReviewerName { get; set; }
    public string? ActionName { get; set; }
    public bool VetoRight { get; set; }
    public SignType SignType { get; set; }
    public DateOnly DateStart { get; set; }

    private DateOnly? _dateEnd;
    public DateOnly? DateEnd
    {
        get => _dateEnd ?? DateStart.AddDays(10);
        set => _dateEnd = value;
    }

    public DateOnly? ResignDateEnd { get; set; }
    public List<ProcessSignDetailRequest> SignDetails { get; set; }
}
public class ProcessSignDetailRequest
{
    public Guid? Id { get; set; }
    public int FileIndex { get; set; }
    public double PosX { get; set; }
    public double PosY { get; set; }
    public double SignZoneWidth { get; set; }
    public double SignZoneHeight { get; set; }
    public int? SignPage { get; set; }
    public double? TranslateX { get; set; }
    public double? TranslateY { get; set; }
}

public class ConfirmProcessRequest
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public ProcessType Type { get; set; }
    public Guid ManagerId { get; set; }
    public bool? BlockchainEnabled { get; set; } = false;
    public string? Description { get; set; } = string.Empty;

    public List<ProcessDetailRequest> ProcessDetails { get; set; }
}

public class ApproveDocumentRequest
{
    public string? DigitalSignaturePassword { get; set; }
    public List<ImageSignatureModel>? ImageSignatures { get; set; }
    public string? Comment { get; set; }

}

public class ImageSignatureModel
{
    public Guid FileId { get; set; }
    public IFormFile? Image { get; set; }
}

public class RejectDocumentRequest
{
    public string Comment { get; set; }
    public int? RollbackStep { get; set; }
}