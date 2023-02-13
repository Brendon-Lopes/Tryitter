using BackEndTryitter.Contracts.User;
using BackEndTryitter.Repositories;
using BackEndTryitter.Services.Authorization;
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

    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    public IActionResult GetUserById([FromRoute] Guid id)
    {
        var user =_userRepository.GetUserById(id);

        if (user == null) return NotFound();

        var response = new GetUserByIdResponse(
            user.UserId,
            user.FullName,
            user.Username,
            user.CurrentModule,
            user.StatusMessage ?? "");

        return Ok(response);
    }

    [HttpPatch]
    [Route("{id}/status")]
    public IActionResult UpdateUserStatus([FromRoute] Guid id, [FromBody] UpdateUserStatusRequest request)
    {
        if (!AuthorizationServices.CheckAuthorization(HttpContext, id)) return Unauthorized();

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

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        if (!AuthorizationServices.CheckAuthorization(HttpContext, id)) return Unauthorized();

        _userRepository.Delete(id);

        return NoContent();
    }
}
