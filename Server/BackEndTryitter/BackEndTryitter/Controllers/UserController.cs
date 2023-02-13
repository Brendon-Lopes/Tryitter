using System.Security.Claims;
using BackEndTryitter.Contracts.User;
using BackEndTryitter.Repositories;
using BackEndTryitter.Services.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndTryitter.Controllers;

[Authorize]
[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPatch]
    [Route("{id}/status")]
    public IActionResult UpdateUserStatus([FromRoute] Guid id, [FromBody] UpdateUserStatusRequest request)
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        if (claimsIdentity == null) return Unauthorized();

        var tokenId = claimsIdentity.Claims.First(c => c.Type == "userId").Value;
        if (tokenId != id.ToString()) return Unauthorized();

        var validator = new UpdateUserStatusValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(new { error = new {
                message = "Validation errors",
                errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
            }});
        }

        _userRepository.UpdateStatus(id, request);

        return NoContent();
    }
}
