using DressedUp.Domain.Aggregates.PostAggregate;
using DressedUp.Domain.Aggregates.ShareAggregate;
using DressedUp.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DressedUp.Infrastructure.Configurations;

public class ShareConfiguration : IEntityTypeConfiguration<Share>
{
    public void Configure(EntityTypeBuilder<Share> builder)
    {
        builder.HasKey(s => s.ShareId);

        builder.Property(s => s.CreatedAt).HasDefaultValueSql("GETDATE()");

        builder.HasOne<Post>()
            .WithMany()
            .HasForeignKey(s => s.PostId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}