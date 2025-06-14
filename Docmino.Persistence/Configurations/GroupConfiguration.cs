using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable(nameof(Group));
        builder.HasKey(x => x.Id);

        builder.HasData(GroupSeed.Groups);
    }
}
