using ETicaret.Application.CQRS.Commands.Adresses;
using ETicaret.Application.CQRS.Queries.Adresses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,User")]
public class AdressController(IMediator mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> AddAdress(AddAdressCommandRequest request)
    {
        var result = await mediator.Send(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetUserAdress(Guid userId)
    {
        var result = await mediator.Send(new GetUserAdressQuery(){UserId = userId});
        return Ok(result);
    }
}