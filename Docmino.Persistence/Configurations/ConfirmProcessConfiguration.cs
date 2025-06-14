using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class ConfirmProcessConfiguration : IEntityTypeConfiguration<ConfirmProcess>
{
    public void Configure(EntityTypeBuilder<ConfirmProcess> builder)
    {
        builder.ToTable(nameof(ConfirmProcess));
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Document).WithOne(d => d.Process).HasForeignKey<ConfirmProcess>(x => x.DocumentId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Manager).WithMany(x => x.ConfirmProcesses).HasForeignKey(x => x.ManagerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasData(ConfirmProcessSeed.ConfirmProcess);


    }
}
