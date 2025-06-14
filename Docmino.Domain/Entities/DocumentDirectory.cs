using Docmino.Domain.Enums;
using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class DocumentDirectory : Entity, IAuditableEntity
{
    public string Name { get; set; }
    public DirectoryType Type { get; set; }
    public Guid? ParentDirectoryId { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    public DocumentDirectory? ParentDirectory { get; set; }
    public ICollection<DocumentDirectory>? Children { get; set; }
    public ICollection<Storage>? Storages { get; set; }
}
