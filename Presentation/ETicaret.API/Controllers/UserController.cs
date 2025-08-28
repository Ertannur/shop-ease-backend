using ETicaret.Application.CQRS.Commands.Users;
using ETicaret.Application.CQRS.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin, User")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateUser(UpdateUserCommandRequest  request)
    {
        var result = await mediator.Send(request);
        if(result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await mediator.Send(new GetCurrentUserQuery());
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var result = await mediator.Send(new GetUserByIdQuery(){Id = id});
        if(result is null)
            return NotFound("User not found");
        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ChangePassword(UserChangePasswordCommandRequest  request)
    {
        var result = await mediator.Send(request);
        if(result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    
}