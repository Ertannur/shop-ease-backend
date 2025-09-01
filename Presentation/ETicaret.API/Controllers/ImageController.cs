using ETicaret.Application.Abstractions.Storage;
using ETicaret.Application.CQRS.Commands.Images;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImageController(IMediator mediator, IStorageService storageService) : ControllerBase
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

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Upload(IFormFileCollection files)
    {
        var images = await storageService.UploadAsync("photo-images", files);
        List<string> list = new List<string>();
        foreach (var item in images)
        {
            list.Add(item.pathOrContainerName);
        }
        return Ok(list);
    }
}