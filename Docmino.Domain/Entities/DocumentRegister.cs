using Docmino.Domain.Enums;
using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class DocumentRegister : Entity, IAuditableEntity
{
    public string Name { get; set; }
    public DocumentType RegisterType { get; set; }
    public int Year { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Document> Documents { get; set; }
}
