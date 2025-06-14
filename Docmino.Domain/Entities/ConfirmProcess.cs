using Docmino.Domain.Enums;
using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class ConfirmProcess : Entity, IAuditableEntity
{
    public Guid DocumentId { get; set; }
    public string? Name { get; set; }
    public ProcessType Type { get; set; }
    public Guid ManagerId { get; set; }
    public bool BlockchainEnabled { get; set; }
    public string Description { get; set; }
    public int CurrentStepNumber { get; set; }
    public ProcessStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public User Manager { get; set; }

    public Document Document { get; set; }
    public ICollection<ProcessDetail> ProcessDetails { get; set; }
    public ICollection<ProcessHistory> ProcessHistories { get; set; }
}
