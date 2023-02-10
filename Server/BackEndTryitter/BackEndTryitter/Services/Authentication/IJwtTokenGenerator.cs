namespace BackEndTryitter.Services.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string fullName, string username);
}