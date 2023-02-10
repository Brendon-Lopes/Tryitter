using BackEndTryitter.Models;
using BackEndTryitter.Repositories;

namespace BackEndTryitter.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Register(string fullName,
        string username,
        string email,
        string password,
        int currentModule)
    {
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new InvalidOperationException("Email already exists");
        }

        var user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = fullName,
            Username = username,
            Email = email,
            Password = password,
            CurrentModule = currentModule
        };

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user.UserId, fullName, username);

        return new AuthenticationResult(user.UserId, fullName, email, username, currentModule, token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not { } user)
        {
            throw new InvalidOperationException("User with given email does not exist");
        }

        if (user.Password != password)
        {
            throw new InvalidOperationException("Password is incorrect");
        }

        var token = _jwtTokenGenerator.GenerateToken(user.UserId, user.FullName, user.Username);

        return new AuthenticationResult(user.UserId, user.FullName, email, user.Username, user.CurrentModule, token);
    }
}