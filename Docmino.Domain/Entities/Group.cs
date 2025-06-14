using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class Group : Entity, IAuditableEntity
{
    public string Name { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Guid CreatedBy { get; set; }
	public Guid UpdatedBy { get; set; }
	public bool IsDeleted { get; set; }

    public IList<UserGroup> UserGroups { get; set; }
    public ICollection<ProcessDetail> ProcessDetails { get; set; }
}
