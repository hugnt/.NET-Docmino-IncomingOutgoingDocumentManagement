namespace Docmino.Application.Models.Internal;
public class DocumentFileAdapter
{
    public string OriginalFileUrl { get; set; }
    public int CurrentProcessStepNumber { get; set; }
    public IEnumerable<ProcessSignHistoryAdapter>? ProcessSignHistories { get; set; }
}
