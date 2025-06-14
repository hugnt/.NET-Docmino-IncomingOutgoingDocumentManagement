using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class Position : CountableEntity, IAuditableEntity
{
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    public string Description { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Guid CreatedBy { get; set; }
	public Guid UpdatedBy { get; set; }
	public bool IsDeleted { get; set; }

    public Department Department { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<ProcessDetail> ProcessDetails { get; set; }
}
