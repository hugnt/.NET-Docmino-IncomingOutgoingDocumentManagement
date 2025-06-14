using Docmino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class ProcessSignHistoryConfiguration : IEntityTypeConfiguration<ProcessSignHistory>
{
    public void Configure(EntityTypeBuilder<ProcessSignHistory> builder)
    {
        builder.ToTable(nameof(ProcessSignHistory));
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.ProcessHistory).WithMany(x => x.ProcessSignHistories).HasForeignKey(x => x.ProcessHistoryId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.OriginalFile).WithMany(df => df.ProcessSignHistories).HasForeignKey(x => x.OriginalFileId).OnDelete(DeleteBehavior.NoAction);
    }
}
