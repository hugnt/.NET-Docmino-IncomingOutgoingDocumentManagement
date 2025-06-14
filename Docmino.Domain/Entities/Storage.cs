using Docmino.Domain.Enums;
using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class Storage : Entity, IAuditableEntity
{
    public string Name { get; set; }
    public Guid DirectoryId { get; set; }
    public int Year { get; set; }
    public int? StoragePeriodId { get; set; }
    public ContainerStatus Status { get; set; }
    public string? Description { get; set; }
    public string Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    public DocumentDirectory Directory { get; set; }
    public StoragePeriod? StoragePeriod { get; set; }

    public ICollection<Document> Documents { get; set; }
}
