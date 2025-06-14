using Docmino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class SystemFeatureConfiguration : IEntityTypeConfiguration<SystemFeature>
{
    public void Configure(EntityTypeBuilder<SystemFeature> builder)
    {
        builder.ToTable(nameof(SystemFeature));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

    }
}
