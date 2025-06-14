using Docmino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class ProcessSignDetailConfiguration : IEntityTypeConfiguration<ProcessSignDetail>
{
    public void Configure(EntityTypeBuilder<ProcessSignDetail> builder)
    {
        builder.ToTable(nameof(ProcessSignDetail));
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.File).WithMany(x => x.ProcessSignDetails).HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.ProcessDetail).WithMany(x => x.ProcessSignDetails).HasForeignKey(x => x.ProcessDetailsId).OnDelete(DeleteBehavior.Cascade);

    }
}
