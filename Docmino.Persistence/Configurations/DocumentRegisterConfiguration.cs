using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class DocumentRegisterConfiguration : IEntityTypeConfiguration<DocumentRegister>
{
    public void Configure(EntityTypeBuilder<DocumentRegister> builder)
    {
        builder.ToTable(nameof(DocumentRegister));
        builder.HasKey(x => x.Id);

        builder.HasData(DocumentRegisterSeed.DocumentRegisters);
    }
}
