using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable(nameof(Document));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.StorageId).IsRequired(false);


        builder.HasOne(x => x.DocumentRegister).WithMany(x => x.Documents).HasForeignKey(x => x.DocumentRegisterId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Category).WithMany(x => x.Documents).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Field).WithMany(x => x.Documents).HasForeignKey(x => x.FieldId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Organization).WithMany(x => x.Documents).HasForeignKey(x => x.OrganizationId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Department).WithMany(x => x.Documents).HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Storage).WithMany(x => x.Documents).HasForeignKey(x => x.StorageId).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(DocumentSeed.Documents);
    }
}
