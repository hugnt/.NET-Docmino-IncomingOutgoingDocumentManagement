using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class Department : CountableEntity, IAuditableEntity
{
    public string Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public int? Id0 { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    public Department? Department0 { get; set; }
    public ICollection<Position> Positions { get; set; }
    public ICollection<ProcessDetail> ProcessDetails { get; set; }
    public ICollection<Document> Documents { get; set; }
}
