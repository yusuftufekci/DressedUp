using DressedUp.Domain.Aggregates.CommentAggregate;
using DressedUp.Domain.Aggregates.FollowAggregate;
using DressedUp.Domain.Aggregates.LikeAggregate;
using DressedUp.Domain.Aggregates.NotificationAggregate;
using DressedUp.Domain.Aggregates.PostAggregate;
using DressedUp.Domain.Aggregates.ShareAggregate;
using DressedUp.Domain.Aggregates.UserAggregate;
using DressedUp.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DressedUp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    // DbSet tanımlamaları
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<Share> Shares { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationType> NotificationTypes { get; set; }   
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}