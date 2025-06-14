using Docmino.Application.Models.Lookups;
using Docmino.Application.Models.Requests;
using Docmino.Domain.Entities;
using System.Linq.Expressions;

namespace Docmino.Application.Models.Mappings;
public static class ConfirmProcessMapping
{
    public static ConfirmProcess ToEntity(this ConfirmProcessRequest request) => new()
    {
        Name = request.Name,
        Type = request.Type,
        ManagerId = request.ManagerId,
        BlockchainEnabled = request.BlockchainEnabled ?? false,
        Description = request.Description ?? ""
    };

    public static readonly Expression<Func<ConfirmProcess, ConfirmProcessLookup>> SelectLookupExpression = x => new ConfirmProcessLookup
    {
        Id = x.Id,
        Name = x.Name ?? string.Empty,
        CurrentStepNumber = x.CurrentStepNumber,
        NextStepNumber = (x.ProcessDetails != null && x.CurrentStepNumber == x.ProcessDetails.Count) ? x.CurrentStepNumber : (x.CurrentStepNumber + 1),
        PreviousStepNumber = x.CurrentStepNumber == 1 ? x.CurrentStepNumber : (x.CurrentStepNumber - 1),
        Status = x.Status
    };
}
