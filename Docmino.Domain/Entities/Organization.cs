using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class Organization : CountableEntity, IAuditableEntity
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string ContactPersonName { get; set; }
    public string Description { get; set; }

	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Guid CreatedBy { get; set; }
	public Guid UpdatedBy { get; set; }
	public bool IsDeleted { get; set; }

    public ICollection<Document> Documents { get; set; }

}
 