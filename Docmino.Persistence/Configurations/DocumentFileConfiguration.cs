using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class DocumentFileConfiguration : IEntityTypeConfiguration<DocumentFile>
{
    public void Configure(EntityTypeBuilder<DocumentFile> builder)
    {
        builder.ToTable(nameof(DocumentFile));
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Document).WithMany(x => x.DocumentFiles).HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.Cascade);

        builder.HasData(DocumentFileSeed.DocumentFiles);
    }
}
