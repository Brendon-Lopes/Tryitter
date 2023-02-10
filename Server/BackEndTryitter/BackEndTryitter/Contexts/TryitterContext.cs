using BackEndTryitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndTryitter.Contexts;

public class TryitterContext : DbContext, ITryitterContext
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
}