using Docmino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class ProcessHistoryConfiguration : IEntityTypeConfiguration<ProcessHistory>
{
    public void Configure(EntityTypeBuilder<ProcessHistory> builder)
    {
        builder.ToTable(nameof(ProcessHistory));
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Process).WithMany(x => x.ProcessHistories).HasForeignKey(x => x.ProcessId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.UserReviewer).WithMany(u => u.ProcessHistories).HasForeignKey(x => x.UserReviewerId).OnDelete(DeleteBehavior.NoAction);
    }
}
