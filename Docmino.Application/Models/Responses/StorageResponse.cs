using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Responses;
public class StorageResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    public Guid BoxId { get; set; }
    public string BoxName { get; set; }
    public int Year { get; set; }
    public int? StoragePeriodId { get; set; }
    public string StoragePeriodName { get; set; }
    public ContainerStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int DocumentCount { get; set; }
}

public class StorageDetailResponse : StorageResponse
{
    public List<PublishDocumentResponse>? Documents { get; set; }
}
