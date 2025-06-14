using Docmino.Domain.Entities;

namespace Docmino.Persistence.SeedData;

public class StoragePeriodSeed
{
    private static readonly DateTime _defaultTime = new(2025, 01, 01);
    public static IEnumerable<StoragePeriod> StoragePeriods => new List<StoragePeriod>()
        {
            new StoragePeriod
            {
                Id = 1,
                Name = "Ngắn hạn (1 năm)",
                YearAmount = 1,
                Description = "Storage period for 1 year.",
                CreatedAt = _defaultTime,
                UpdatedAt = _defaultTime,
                CreatedBy = Guid.Empty,
                UpdatedBy = Guid.Empty,
                IsDeleted = false,
            },
            new StoragePeriod
            {
                Id = 2,
                Name = "5 năm",
                YearAmount = 5,
                Description = "Storage period for 5 years.",
                CreatedAt = _defaultTime,
                UpdatedAt = _defaultTime,
                CreatedBy = Guid.Empty,
                UpdatedBy = Guid.Empty,
                IsDeleted = false,
            },
            new StoragePeriod
            {
                Id = 3,
                Name = "10 năm",
                YearAmount = 10,
                Description = "Storage period for 10 years.",
                CreatedAt = _defaultTime,
                UpdatedAt = _defaultTime,
                CreatedBy = Guid.Empty,
                UpdatedBy = Guid.Empty,
                IsDeleted = false,
            },
            new StoragePeriod
            {
                Id = 4,
                Name = "Vĩnh viễn",
                YearAmount = int.MaxValue,
                Description = "Storage period with no expiration (forever).",
                CreatedAt = _defaultTime,
                UpdatedAt = _defaultTime,
                CreatedBy = Guid.Empty,
                UpdatedBy = Guid.Empty,
                IsDeleted = false,
            }
        };
}
