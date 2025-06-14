using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class StoragePeriod : CountableEntity, IAuditableEntity
{
    public string Name { get; set; }
    public int YearAmount { get; set; }
    public string Description { get; set; }

	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Guid CreatedBy { get; set; }
	public Guid UpdatedBy { get; set; }
	public bool IsDeleted { get; set; }

    public ICollection<Storage> Storages { get; set; }
}
