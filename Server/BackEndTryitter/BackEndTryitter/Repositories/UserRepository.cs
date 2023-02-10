using BackEndTryitter.Contexts;
using BackEndTryitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndTryitter.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ITryitterContext _context;

    public UserRepository(ITryitterContext context)
    {
        _context = context;
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public void Add(User user)
    {
        _context.Users.Add(user);

        _context.SaveChanges();
    }
}