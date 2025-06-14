using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable(nameof(Position));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Description).IsRequired(false);

        builder.HasOne(x => x.Department).WithMany(d => d.Positions).HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.Restrict);

        builder.HasData(PositionSeed.Positions);

    }
}
