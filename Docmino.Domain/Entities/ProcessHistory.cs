using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class ProcessHistory : Entity
{
    public Guid ProcessId { get; set; }
    public string? ProcessName { get; set; }
    public int CurrentStepNumber { get; set; }
    public string? CurrentStatusName { get; set; }
    public string ReviewerName { get; set; }
    public Guid UserReviewerId { get; set; }
    public string? Comment { get; set; }
    public int NextStepNumber { get; set; }
    public string? ActionName { get; set; }
    public string? TxHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public ConfirmProcess Process { get; set; }
    public User UserReviewer { get; set; }
    public ICollection<ProcessSignHistory> ProcessSignHistories { get; set; }

}
