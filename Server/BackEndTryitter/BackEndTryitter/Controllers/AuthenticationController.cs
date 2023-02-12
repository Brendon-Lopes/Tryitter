using BackEndTryitter.Contracts.Authentication;
using BackEndTryitter.Models;
using BackEndTryitter.Services.Authentication;
using BackEndTryitter.Services.Validators;
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
        var validator = new RegisterUserValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(new { error = new {
                message = "Validation errors",
                errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
            }});
        }

        var authResult = _authenticationService.Register(request);

        var response = new AuthenticationResponse(
            authResult.User.UserId,
            authResult.User.FullName,
            authResult.User.Username,
            authResult.User.Email,
            authResult.User.CurrentModule,
            authResult.Token);

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var validator = new LoginValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(new { error = new {
                message = "Validation errors",
                errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
            }});
        }

        var authResult = _authenticationService.Login(request);

        var response = new AuthenticationResponse(
            authResult.User.UserId,
            authResult.User.FullName,
            authResult.User.Username,
            authResult.User.Email,
            authResult.User.CurrentModule,
            authResult.Token);

        return Ok(response);
    }
}
