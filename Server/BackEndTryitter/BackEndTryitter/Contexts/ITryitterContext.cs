using BackEndTryitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndTryitter.Contexts;

public interface ITryitterContext
{
    DbSet<User> Users { get; set; }
    DbSet<Post> Posts { get; set; }
    DbSet<Image> Images { get; set; }
    int SaveChanges();
}