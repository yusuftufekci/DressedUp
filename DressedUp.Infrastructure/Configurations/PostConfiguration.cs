using DressedUp.Domain.Aggregates.PostAggregate;
using DressedUp.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DressedUp.Infrastructure.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.PostId);

        builder.Property(p => p.Caption).HasColumnType("TEXT");
        builder.Property(p => p.IsVideo).HasDefaultValue(false);
        builder.Property(p => p.MediaUrl).HasMaxLength(255);
        builder.Property(p => p.LikeCount).HasDefaultValue(0);
        builder.Property(p => p.ShareCount).HasDefaultValue(0);
        builder.Property(p => p.CommentCount).HasDefaultValue(0);

        builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
        builder.Property(p => p.UpdatedAt).HasDefaultValueSql("GETDATE()");

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}