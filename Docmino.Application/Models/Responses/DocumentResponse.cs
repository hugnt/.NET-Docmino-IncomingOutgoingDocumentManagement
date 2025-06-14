using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Responses;
public class DocumentLookupResponse
{
    public List<DateOnly>? ArrivalDates { get; set; }
    public List<DateOnly>? IssuedDates { get; set; }
    public List<Lookup<int>>? Categories { get; set; }
    public List<Lookup<int>>? Departments { get; set; }
    public List<Lookup<int>>? Fields { get; set; }
    public List<Lookup<int>>? DocumentStatus { get; set; }
    public List<Lookup<Guid>>? DocumentRegisters { get; set; }
    public List<Lookup<int>>? Organizations { get; set; }
    public List<Lookup<int>>? SecurePriorities { get; set; }
    public List<Lookup<int>>? UrgentPriorities { get; set; }

    public List<Lookup<int>>? ProcessTypes { get; set; }
    public List<Lookup<int>>? ReviewerTypes { get; set; }
    public List<Lookup<int>>? SignTypes { get; set; }

    public List<Lookup<Guid>>? ProcessManagers { get; set; }
}
public class ReviewerLookupResponse
{
    public List<Lookup<Guid>>? Groups { get; set; }
    public List<Lookup<int>>? Departments { get; set; }
    public List<Lookup<int>>? Positions { get; set; }
    public List<Lookup<Guid>>? Users { get; set; }
}
public class ProcessingDocumentResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string DocumentRegisterName { get; set; }
    public DocumentType DocumentType { get; set; }
    public int CurrentStepNumber { get; set; }
    public int TotalStepNumber { get; set; }
    public string CodeNotation { get; set; }
    public string CategoryName { get; set; }
    public DateOnly IssuedDate { get; set; }
}

public class PublishDocumentResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string CodeNotation { get; set; }
    public string DocumentRegisterName { get; set; }
    public string CategoryName { get; set; }
    public string FieldName { get; set; }
    public DocumentType DocumentType { get; set; }
    public DateOnly IssuedDate { get; set; }
    public DateTime PublishDate { get; set; }
}

public class SignedDocumentResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string DocumentRegisterName { get; set; }
    public DocumentType DocumentType { get; set; }
    public int CurrentStepNumber { get; set; }
    public int NextStepNumber { get; set; }
    public string CodeNotation { get; set; }
    public bool IsApproved { get; set; }
    public string ActionName { get; set; }
    public DateTime ApprovedAt { get; set; }
}

public class DocumentDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Guid DocumentRegisterId { get; set; }
    public int FieldId { get; set; }
    public string? CodeNumber { get; set; }
    public string? CodeNotation { get; set; }
    public DateOnly IssuedDate { get; set; }
    public int OrganizationId { get; set; }
    public string? Subject { get; set; }
    public int PageAmount { get; set; }
    public string? Description { get; set; }
    public SecurePriority SecurePriority { get; set; }
    public UrgentPriority UrgentPriority { get; set; }

    public string ArrivalNumber { get; set; }
    public DateOnly? ArrivalDate { get; set; }
    public string? ToPlaces { get; set; }
    public DateOnly? DueDate { get; set; }
    public int IssuedAmount { get; set; }

    public DocumentStatus DocumentStatus { get; set; }

    public List<DocumentFileResponse>? DocumentFiles { get; set; }
    public ConfirmProcessResponse? ConfirmProcess { get; set; }
}