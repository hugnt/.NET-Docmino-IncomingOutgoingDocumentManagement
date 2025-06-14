using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable(nameof(Department));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Code).IsRequired(false);
        builder.Property(x => x.Description).IsRequired(false);

        builder.HasOne(x => x.Department0).WithMany().HasForeignKey(d => d.Id0).OnDelete(DeleteBehavior.Restrict);

        builder.HasData(DepartmentSeed.Departments);
    }
}
