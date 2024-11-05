using DressedUp.Domain.Aggregates.CommentAggregate;
using DressedUp.Domain.Aggregates.NotificationAggregate;
using DressedUp.Domain.Aggregates.PostAggregate;
using DressedUp.Domain.Aggregates.ShareAggregate;
using DressedUp.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DressedUp.Infrastructure.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.NotificationId);

        builder.Property(n => n.Content).HasColumnType("TEXT");
        builder.Property(n => n.IsRead).HasDefaultValue(false);
        builder.Property(n => n.CreatedAt).HasDefaultValueSql("GETDATE()");

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<NotificationType>()
            .WithMany()
            .HasForeignKey(n => n.NotificationTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Post>()
            .WithMany()
            .HasForeignKey(n => n.PostId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Comment>()
            .WithMany()
            .HasForeignKey(n => n.CommentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Share>()
            .WithMany()
            .HasForeignKey(n => n.ShareId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}