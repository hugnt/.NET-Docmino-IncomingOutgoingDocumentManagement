using Docmino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class StorageConfiguration : IEntityTypeConfiguration<Storage>
{
    public void Configure(EntityTypeBuilder<Storage> builder)
    {
        builder.ToTable(nameof(Storage));
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Code).IsUnique();

        builder.HasOne(x => x.Directory).WithMany(d => d.Storages).HasForeignKey(x => x.DirectoryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.StoragePeriod).WithMany(sp => sp.Storages).HasForeignKey(x => x.StoragePeriodId).OnDelete(DeleteBehavior.SetNull);
    }
}
