using BackEndTryitter.Models;
using BackEndTryitter.Repositories;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

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
            throw new DbUpdateException("Email already exists");
        }

        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        var user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = fullName,
            Username = username,
            Email = email,
            Password = hashedPassword,
            CurrentModule = currentModule
        };

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not { } user)
        {
            throw new DbUpdateException("User with given email does not exist");
        }

        var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!isPasswordCorrect)
        {
            throw new DbUpdateException("Password is incorrect");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}