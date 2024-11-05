using DressedUp.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DressedUp.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Name).IsRequired().HasMaxLength(90);
        builder.Property(u => u.Lastname).IsRequired().HasMaxLength(90);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);

        builder.Property(u => u.Bio).HasColumnType("TEXT");
        builder.Property(u => u.MobileNo).HasMaxLength(14);
        builder.Property(u => u.ProfilePicUrl).HasMaxLength(255);

        builder.Property(u => u.IsProfileHidden).HasDefaultValue(false);
        builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
        builder.Property(u => u.UpdatedAt).HasDefaultValueSql("GETDATE()");
    }
}