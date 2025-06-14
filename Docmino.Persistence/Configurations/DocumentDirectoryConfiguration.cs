using Docmino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class DocumentDirectoryConfiguration : IEntityTypeConfiguration<DocumentDirectory>
{
    public void Configure(EntityTypeBuilder<DocumentDirectory> builder)
    {
        builder.ToTable(nameof(DocumentDirectory));
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.ParentDirectory).WithMany(pd => pd.Children).HasForeignKey(d => d.ParentDirectoryId).OnDelete(DeleteBehavior.Restrict);
    }
}
