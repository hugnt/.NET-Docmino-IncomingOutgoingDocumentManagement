namespace Docmino.Application.Models.Internal;
public class ReviewerModel
{
    public Guid UserId { get; set; }
    public int PositionId { get; set; }
    public int DepartmentId { get; set; }
    public List<Guid> GroupIds { get; set; }
}
