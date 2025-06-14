using Docmino.Domain.Enums;
using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class ProcessDetail : Entity
{
    public Guid ProcessId { get; set; }
    public int StepNumber { get; set; }
    public Guid? ReviewerUserId { get; set; }
    public Guid? ReviewerGroupId { get; set; }
    public int? ReviewerPositionId { get; set; }
    public int? ReviewerDepartmentId { get; set; }
    public ReviewerType ReviewerType { get; set; }

    public SignType SignType { get; set; }
    public bool VetoRight { get; set; }

    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public DateOnly? ResignDateEnd { get; set; }

    public string? ReviewerName { get; set; }
    public string? ActionName { get; set; }

    public ConfirmProcess Process { get; set; }
    public User ReviewerUser { get; set; }
    public Group ReviewerGroup { get; set; }
    public Position ReviewerPosition { get; set; }
    public Department ReviewerDepartment { get; set; }

    public ICollection<ProcessSignDetail> ProcessSignDetails { get; set; }

}
