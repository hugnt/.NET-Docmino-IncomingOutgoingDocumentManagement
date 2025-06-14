using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Requests;


public class ExternalDocumentFilterRequest : FilterRequest
{
    public DocumentType? DocumentType { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public List<DateOnly>? ArrivalDates { get; set; }
    public List<int>? Categories { get; set; }
    public List<int>? Fields { get; set; }
    public List<DocumentStatus>? DocumentStatus { get; set; }
    public List<Guid>? DocumentRegisters { get; set; }
}


public class ExternalDocumentRequest
{
    public string Name { get; set; }
    public string CodeNotation { get; set; }
    public Guid DocumentRegisterId { get; set; }
    public int CategoryId { get; set; }
    public int FieldId { get; set; }
    public int OrganizationId { get; set; }
    public string? Subject { get; set; }
    public string? ToPlaces { get; set; }
    public string? Description { get; set; }
    public DateOnly IssuedDate { get; set; }
    public int PageAmount { get; set; }
    public int IssuedAmount { get; set; }
    public SecurePriority SecurePriority { get; set; }
    public UrgentPriority UrgentPriority { get; set; }
    public DocumentStatus? Status { get; set; }
    public List<DocumentFileRequest> DocumentFiles { get; set; }
    public ConfirmProcessRequest ConfirmProcess { get; set; }
}

public class ExternalIncomingDocumentRequest : ExternalDocumentRequest
{
    public string ArrivalNumber { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public DateOnly? DueDate { get; set; }
}

public class ExternalOutgoingDocumentRequest : ExternalDocumentRequest
{
    public string CodeNumber { get; set; }
}

