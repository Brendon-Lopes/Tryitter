using BackEndTryitter.Contracts.User;
using BackEndTryitter.Models;

namespace BackEndTryitter.Repositories;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    User? GetUserByUsername(string username);
    User? GetUserById(Guid id);
    void Add(User user);
    void UpdateStatus(Guid id, UpdateUserStatusRequest request);
    void Delete(Guid id);
}