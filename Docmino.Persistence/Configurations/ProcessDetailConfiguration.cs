using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class ProcessDetailConfiguration : IEntityTypeConfiguration<ProcessDetail>
{
    public void Configure(EntityTypeBuilder<ProcessDetail> builder)
    {
        builder.ToTable(nameof(ProcessDetail));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ReviewerUserId).IsRequired(false);
        builder.Property(x => x.ReviewerGroupId).IsRequired(false);
        builder.Property(x => x.ReviewerPositionId).IsRequired(false);
        builder.Property(x => x.ReviewerDepartmentId).IsRequired(false);

        builder.HasOne(x => x.Process).WithMany(x => x.ProcessDetails).HasForeignKey(x => x.ProcessId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.ReviewerUser).WithMany(x => x.ProcessDetails).HasForeignKey(x => x.ReviewerUserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.ReviewerGroup).WithMany(x => x.ProcessDetails).HasForeignKey(x => x.ReviewerGroupId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.ReviewerPosition).WithMany(x => x.ProcessDetails).HasForeignKey(x => x.ReviewerPositionId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.ReviewerDepartment).WithMany(x => x.ProcessDetails).HasForeignKey(x => x.ReviewerDepartmentId).OnDelete(DeleteBehavior.NoAction);

        builder.HasData(ProcessDetailSeed.ProcessDetails);
    }
}
