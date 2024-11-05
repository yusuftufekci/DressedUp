using DressedUp.Domain.Aggregates.CommentAggregate;
using DressedUp.Domain.Aggregates.PostAggregate;
using DressedUp.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DressedUp.Infrastructure.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.CommentId);

        builder.Property(c => c.Content)
            .IsRequired()
            .HasColumnType("TEXT");

        builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");
        builder.Property(c => c.UpdatedAt).HasDefaultValueSql("GETDATE()");

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Post>()
            .WithMany()
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}