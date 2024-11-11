using DressedUp.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DressedUp.Infrastructure.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Tablo adı
        builder.ToTable("RefreshToken");

        // Primary key
        builder.HasKey(rt => rt.Id);

        // Token column
        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(255);

        // ExpiryDate column
        builder.Property(rt => rt.ExpiryDate)
            .IsRequired();

        // CreatedDate column
        builder.Property(rt => rt.CreatedDate)
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        // IsRevoked and IsUsed columns
        builder.Property(rt => rt.IsRevoked)
            .HasDefaultValue(false)
            .IsRequired();
        builder.Property(rt => rt.IsUsed)
            .HasDefaultValue(false)
            .IsRequired();

        // CreatedByIp and RevokedByIp columns
        builder.Property(rt => rt.CreatedByIp)
            .HasMaxLength(45);
        builder.Property(rt => rt.RevokedByIp)
            .HasMaxLength(45);

        // Foreign Key for User
        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(u => u.UserId) 
            .HasColumnName("user_id"); 
        builder.Property(rt => rt.DeviceId)
            .IsRequired()            // NOT NULL olarak ayarlanır
            .HasMaxLength(36)        // UUID formatında 36 karakter uzunluğunda
            .IsFixedLength(true);
        builder.Property(u => u.DeviceId) 
            .HasColumnName("device_id"); 
    }
}