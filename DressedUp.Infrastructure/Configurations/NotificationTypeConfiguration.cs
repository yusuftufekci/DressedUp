using DressedUp.Domain.Aggregates.NotificationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DressedUp.Infrastructure.Configurations;

public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
{
    public void Configure(EntityTypeBuilder<NotificationType> builder)
    {
        builder.HasKey(nt => nt.NotificationTypeId);

        builder.Property(nt => nt.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(nt => nt.Description).HasColumnType("TEXT");
    }
}