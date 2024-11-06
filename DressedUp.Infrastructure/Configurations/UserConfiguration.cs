using DressedUp.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DressedUp.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users"); // Tablo adının doğru olduğundan emin olun
        builder.HasKey(u => u.UserId);
        builder.Property(u => u.UserId) // 
            .HasColumnName("user_id"); 
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("username");
        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100).HasColumnName("email");
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Name).IsRequired().HasMaxLength(90)
            .HasColumnName("name");
        builder.Property(u => u.Lastname).IsRequired().HasMaxLength(90)
            .HasColumnName("lastname");
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255)
            .HasColumnName("password_hash");

        builder.Property(u => u.Bio).HasColumnType("TEXT")
            .HasColumnName("bio");
        builder.Property(u => u.MobileNo).HasMaxLength(14)
            .HasColumnName("mobileno");
        builder.Property(u => u.ProfilePicUrl).HasMaxLength(255)
            .HasColumnName("profile_pic_url");

        builder.Property(u => u.IsProfileHidden).HasDefaultValue(false)
            .HasColumnName("is_profile_hidden");
        builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()")
            .HasColumnName("created_at");
        builder.Property(u => u.UpdatedAt).HasDefaultValueSql("GETDATE()")
            .HasColumnName("updated_at");
    }
}