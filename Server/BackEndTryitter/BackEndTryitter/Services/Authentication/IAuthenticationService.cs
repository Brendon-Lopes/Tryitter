using BackEndTryitter.Contracts.Authentication;

namespace BackEndTryitter.Services.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Register(RegisterRequest request);
    AuthenticationResult Login(string email, string password);
}