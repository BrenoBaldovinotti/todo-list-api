using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
public class UserController(IAuthorizationService authorizationService, IMediator mediator) : BaseController(authorizationService)
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
    {
        try
        {
            var token = await mediator.Send(loginCommand);
            return Success(new { Token = token });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost("register")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> RegisterUser([FromBody] AddUserCommand addUserCommand)
    {
        var userId = await mediator.Send(addUserCommand);
        return Success(new { UserId = userId });
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await mediator.Send(new DeleteUserCommand(id));
        return Success();
    }
}
