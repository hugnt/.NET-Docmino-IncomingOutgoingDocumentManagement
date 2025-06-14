using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class ProcessSignDetail : Entity
{
    public Guid ProcessDetailsId { get; set; }
    public Guid FileId { get; set; }
    public double PosX { get; set; }
    public double PosY { get; set; }
    public double SignZoneWidth { get; set; }
    public double SignZoneHeight { get; set; }
    public int SignPage { get; set; }
    public double TranslateX { get; set; }
    public double TranslateY { get; set; }
    public DocumentFile File { get; set; }
    public ProcessDetail ProcessDetail { get; set; }
}
