namespace BackEndTryitter.Services.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Register(string fullName, string username, string email, string password, int currentModule);
    AuthenticationResult Login(string email, string password);
}