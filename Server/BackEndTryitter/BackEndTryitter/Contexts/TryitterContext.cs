using BackEndTryitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndTryitter.Contexts;

public class TryitterContext : DbContext
{
    public TryitterContext(DbContextOptions<TryitterContext> options)
        : base(options)
    { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_CONNECTION_STRING");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(u => u.CreatedAt)
            .HasDefaultValueSql("getdate()")
            .HasConversion<DateTime>()
            .HasColumnType("datetime2")
            .IsRequired();

        modelBuilder.Entity<User>().Property(u => u.UpdatedAt)
            .HasDefaultValueSql("getdate()")
            .HasConversion<DateTime>()
            .HasColumnType("datetime2")
            .IsRequired();
    }
}