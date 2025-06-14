using Docmino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docmino.Persistence.Configurations;

public sealed class UserFeatureConfiguration : IEntityTypeConfiguration<UserFeature>
{
    public void Configure(EntityTypeBuilder<UserFeature> builder)
    {
        builder.ToTable(nameof(UserFeature));
        builder.HasKey(x => new { x.UserId, x.FeatureId });

        builder.HasOne(x => x.User).WithMany(u => u.UserFeatures).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Feature).WithMany(f => f.UserFeatures).HasForeignKey(u => u.FeatureId).OnDelete(DeleteBehavior.Restrict);
    }
}
