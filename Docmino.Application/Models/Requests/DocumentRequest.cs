using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Requests;
public class ProcessingDocumentFilterRequest : FilterRequest
{
    public DocumentType? DocumentType { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

}

public class DocumentFilterRequest : FilterRequest
{
    public DocumentType? DocumentType { get; set; }
    public string? CodeNotation { get; set; }
    public Guid? StorageId { get; set; }
}
