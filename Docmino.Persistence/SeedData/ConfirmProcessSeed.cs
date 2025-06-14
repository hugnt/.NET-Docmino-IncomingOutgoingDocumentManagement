using Docmino.Domain.Entities;
using Docmino.Domain.Enums;

namespace Docmino.Persistence.SeedData;

public class ConfirmProcessSeed
{
    private static readonly DateTime _defaultTime = new(2025, 01, 01);
    public static IEnumerable<ConfirmProcess> ConfirmProcess => new List<ConfirmProcess>()
    {
        new ConfirmProcess
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            DocumentId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Name = "Secure Approval Process",
            Type = ProcessType.Secure,
            ManagerId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            BlockchainEnabled = true,
            Description = "A secure process for high-value document approvals.",
            Status = ProcessStatus.None,
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new ConfirmProcess
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            DocumentId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Name = "Normal Review Process",
            Type = ProcessType.Normal,
            ManagerId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            BlockchainEnabled = false,
            Description = "Standard review process for regular documents.",
            Status = ProcessStatus.None,
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new ConfirmProcess
        {
            Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            DocumentId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            Name = "Important Contract Process",
            Type = ProcessType.Important,
            ManagerId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            BlockchainEnabled = true,
            Description = "Process for handling important contracts with blockchain logging.",
            Status = ProcessStatus.None,
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        }
    };
}
