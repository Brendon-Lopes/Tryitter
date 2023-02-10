using BackEndTryitter.Models;

namespace BackEndTryitter.Services.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}