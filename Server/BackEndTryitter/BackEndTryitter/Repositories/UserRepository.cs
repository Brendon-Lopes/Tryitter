using System.Net;
using BackEndTryitter.Contexts;
using BackEndTryitter.Contracts.User;
using BackEndTryitter.Exceptions;
using BackEndTryitter.Models;

namespace BackEndTryitter.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TryitterContext _context;

    public UserRepository(TryitterContext context)
    {
        _context = context;
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public User? GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    public User? GetUserById(Guid id)
    {
        return _context.Users.FirstOrDefault(u => u.UserId == id);
    }

    public void Add(User user)
    {
        _context.Users.Add(user);

        _context.SaveChanges();
    }

    public void UpdateStatus(Guid id, UpdateUserStatusRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == id);

        if (user == null)
            throw new CustomException(HttpStatusCode.NotFound, "User not found");

        user.StatusMessage = request.Status;
        user.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == id);

        if (user == null)
            throw new CustomException(HttpStatusCode.NotFound, "User not found");

        _context.Users.Remove(user);

        _context.SaveChanges();
    }
}
