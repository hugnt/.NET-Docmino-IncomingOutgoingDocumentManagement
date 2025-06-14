using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class StoragePeriodConfiguration : IEntityTypeConfiguration<StoragePeriod>
{
    public void Configure(EntityTypeBuilder<StoragePeriod> builder)
    {
        builder.ToTable(nameof(StoragePeriod));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasData(StoragePeriodSeed.StoragePeriods);
    }
}
