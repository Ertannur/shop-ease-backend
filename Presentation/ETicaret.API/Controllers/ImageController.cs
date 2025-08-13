using ETicaret.Application.CQRS.Commands.Images;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImageController(IMediator mediator) : ControllerBase
{
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> UploadImage(UploadImageCommandRequest  request)
    {
        var result = await mediator.Send(request);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}