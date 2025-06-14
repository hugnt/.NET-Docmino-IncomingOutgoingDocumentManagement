using Docmino.Domain.Enums;
using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class DocumentFile : Entity, IAuditableEntity
{
    public Guid DocumentId { get; set; }
    public string FileName { get; set; }
    public string? FileType { get; set; }
    public string FileUrl { get; set; }
    public double FileSize { get; set; }
    public FileEncoding? FileEncoding { get; set; }
    public string? Content { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    public Document Document { get; set; }

    public ICollection<ProcessSignDetail> ProcessSignDetails { get; set; }
    public ICollection<ProcessSignHistory> ProcessSignHistories { get; set; }
}
