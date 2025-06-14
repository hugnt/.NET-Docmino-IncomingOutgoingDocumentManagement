using Docmino.Domain.Entities;
using Docmino.Domain.Enums;

namespace Docmino.Persistence.SeedData;

public class ProcessDetailSeed
{
    private static readonly DateTime _defaultTime = new(2025, 01, 01);
    public static IEnumerable<ProcessDetail> ProcessDetails => new List<ProcessDetail>
    {
        // Example: Replace these with actual ConfirmProcess Ids from ConfirmProcessSeed
        new ProcessDetail
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            ProcessId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // ConfirmProcess 1
            StepNumber = 1,
            ReviewerUserId = null,
            ReviewerGroupId = null,
            ReviewerPositionId = 1,
            ReviewerDepartmentId = null,
            ReviewerType = ReviewerType.Position,
            SignType = SignType.None,
            VetoRight = false,
            DateStart = DateOnly.FromDateTime(_defaultTime),
            DateEnd = DateOnly.FromDateTime(_defaultTime.AddDays(7)),
            ResignDateEnd = DateOnly.FromDateTime(_defaultTime.AddDays(10))
        },
        new ProcessDetail
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            ProcessId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // ConfirmProcess 1
            StepNumber = 2,
            ReviewerUserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            ReviewerGroupId = null,
            ReviewerPositionId = null,
            ReviewerDepartmentId = null,
            ReviewerType = ReviewerType.User,
            SignType = SignType.Image,
            VetoRight = true,
            DateStart = DateOnly.FromDateTime(_defaultTime.AddDays(8)),
            DateEnd = DateOnly.FromDateTime(_defaultTime.AddDays(14)),
            ResignDateEnd = DateOnly.FromDateTime(_defaultTime.AddDays(17))
        }
    };
}
