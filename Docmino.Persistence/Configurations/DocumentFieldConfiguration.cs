using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class DocumentFieldConfiguration : IEntityTypeConfiguration<DocumentField>
{
    public void Configure(EntityTypeBuilder<DocumentField> builder)
    {
        builder.ToTable(nameof(DocumentField));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasData(DocumentFieldSeed.DocumentFields);
    }
}
