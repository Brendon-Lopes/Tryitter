using BackEndTryitter.Models;

namespace BackEndTryitter.Repositories;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    User? GetUserByUsername(string username);
    void Add(User user);
}