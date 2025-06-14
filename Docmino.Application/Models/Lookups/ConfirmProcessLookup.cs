using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Lookups;
public class ConfirmProcessLookup
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CurrentStepNumber { get; set; }
    public int NextStepNumber { get; set; }
    public int PreviousStepNumber { get; set; }
    public ProcessStatus Status { get; set; }

}
