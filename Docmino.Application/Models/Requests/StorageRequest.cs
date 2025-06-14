using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Requests;
public class StorageRequest
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    public Guid BoxId { get; set; }
    public int Year { get; set; }
    public int StoragePeriodId { get; set; }
    public ContainerStatus Status { get; set; }

}

public class StorageDocumentsRequest
{
    public List<Guid> ListDocumentIds { get; set; }

}
public class StorageFilterRequest : FilterRequest
{
    public Guid? BoxId { get; set; }
}