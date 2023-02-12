using System.Net;
using BackEndTryitter.Contracts.Authentication;
using BackEndTryitter.Exceptions;
using BackEndTryitter.Models;
using BackEndTryitter.Repositories;
using BackEndTryitter.Services.Validators;
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

    public AuthenticationResult Register(RegisterRequest request)
    {
        if (_userRepository.GetUserByEmail(request.Email) is not null)
        {
            throw new CustomException(HttpStatusCode.Conflict, "Email already exists");
        }

        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

        var user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = request.FullName,
            Username = request.Username,
            Email = request.Email,
            Password = hashedPassword,
            CurrentModule = request.CurrentModule
        };

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not { } user)
        {
            throw new CustomException(HttpStatusCode.BadRequest, "User with given email does not exist");
        }

        var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!isPasswordCorrect)
        {
            throw new CustomException(HttpStatusCode.BadRequest, "Password is incorrect");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}