using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.ToTable(nameof(UserGroup));
        builder.HasKey(x => new { x.GroupId, x.UserId });

        builder.HasOne(x => x.User).WithMany(u => u.UserGroups).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Group).WithMany(f => f.UserGroups).HasForeignKey(u => u.GroupId).OnDelete(DeleteBehavior.Restrict);

        builder.HasData(UserGroupSeed.UserGroups);
    }
}
