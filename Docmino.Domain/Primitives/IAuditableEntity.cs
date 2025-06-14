namespace Docmino.Domain.Primitives;

public interface IAuditableEntity
{
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
	public bool IsDeleted { get; set; }	
}
