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
    public async Task<IActionResult> GetUserAdress()
    {
        var result = await mediator.Send(new GetUserAdressQuery());
        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateAdress(UpdateAdressCommandRequest  request)
    {
        var result = await mediator.Send(request);
        if(result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> DeleteAdress(DeleteAdressCommandRequest  request)
    {
        var result = await mediator.Send(request);
        if(result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}