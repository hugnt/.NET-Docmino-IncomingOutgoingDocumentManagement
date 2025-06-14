using Docmino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;
public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(nameof(RefreshToken));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Token).IsRequired();
        builder.Property(x => x.JwtId).IsRequired();
        builder.Property(x => x.IssuedAt).IsRequired();
        builder.Property(x => x.ExpireAt).IsRequired();
        builder.Property(x => x.IsRevoked).IsRequired();
        builder.Property(x => x.IsUsed).IsRequired();
    }
}