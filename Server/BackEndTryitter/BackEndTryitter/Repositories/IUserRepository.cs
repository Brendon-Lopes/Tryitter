using BackEndTryitter.Models;

namespace BackEndTryitter.Repositories;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}