using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    public DbSet<User> Users { get; set; }
    public DbSet<PortfolioItem> Portfolio { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<User>().HasIndex(u => u.Username).IsUnique();
         mb.Entity<User>().Property(u => u.Role).HasDefaultValue("manager");
        mb.Entity<PortfolioItem>().Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}