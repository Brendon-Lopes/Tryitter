using BackEndTryitter.Contracts.Authentication;
using BackEndTryitter.Models;
using BackEndTryitter.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BackEndTryitter.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(
            request.FullName,
            request.Username,
            request.Email,
            request.Password,
            request.CurrentModule);

        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FullName,
            authResult.UserName,
            authResult.Email,
            authResult.CurrentModule,
            authResult.Token);

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            request.Email,
            request.Password);

        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FullName,
            authResult.UserName,
            authResult.Email,
            authResult.CurrentModule,
            authResult.Token);

        return Ok(response);
    }
}
