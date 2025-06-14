using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class ProcessSignHistory : Entity
{
    public Guid ProcessHistoryId { get; set; }
    public Guid OriginalFileId { get; set; }
    public string FileUrl { get; set; }
    public ProcessHistory ProcessHistory { get; set; }
    public DocumentFile OriginalFile { get; set; }
}
