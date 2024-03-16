using Forum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forum.database;

public class ForumDbContext : IdentityDbContext<User>
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Theme>? Themes { get; set; }
    public DbSet<Answer>? Answers { get; set; }
    
    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(u => u.Themes)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<User>()
            .HasMany(u => u.Answers)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<Theme>()
            .HasMany(u => u.Answers)
            .WithOne(p => p.Theme)
            .HasForeignKey(p => p.ThemeId);
        
        base.OnModelCreating(builder);
    }
}