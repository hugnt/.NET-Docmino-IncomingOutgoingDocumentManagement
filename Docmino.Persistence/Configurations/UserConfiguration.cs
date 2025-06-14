using Docmino.Domain.Entities;
using Docmino.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Fullname).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PasswordHash).IsRequired();

        builder.Property(x => x.WalletAddress).IsRequired(false);

        builder.HasData(UserSeed.Users);

        builder.HasOne(x => x.Role).WithMany(r => r.Users).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Position).WithMany(p => p.Users).HasForeignKey(x => x.PositionId).OnDelete(DeleteBehavior.Restrict);
    }
}
