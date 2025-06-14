using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Responses;


public class InternalDocumentResponse
{
    public Guid Id { get; set; }
    public string CodeNotation { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public string DocumentRegisterName { get; set; }
    public string FieldName { get; set; }
    public DateOnly IssuedDate { get; set; }
    public string Description { get; set; }
    public string DepartmentName { get; set; }
    public DateOnly? ArrivalDate { get; set; }
    public string? ArrivalNumber { get; set; }
    public string? CodeNumber { get; set; }
    public DocumentStatus DocumentStatus { get; set; }
}

public class InternalDocumentDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Guid DocumentRegisterId { get; set; }
    public int FieldId { get; set; }
    public string CodeNotation { get; set; }
    public DateOnly IssuedDate { get; set; }
    public int DepartmentId { get; set; }
    public string Subject { get; set; }
    public int PageAmount { get; set; }
    public string Description { get; set; }
    public SecurePriority SecurePriority { get; set; }
    public UrgentPriority UrgentPriority { get; set; }

    public string? ArrivalNumber { get; set; }
    public DateOnly? ArrivalDate { get; set; }
    public string? CodeNumber { get; set; }
    public string ToPlaces { get; set; }
    public DateOnly? DueDate { get; set; }
    public int IssuedAmount { get; set; }

    public DocumentStatus DocumentStatus { get; set; }

    public List<DocumentFileResponse>? DocumentFiles { get; set; }
    public ConfirmProcessResponse? ConfirmProcess { get; set; }
}


