using ETicaret.Application.CQRS.Commands.Stock;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StockController(IMediator mediator) : ControllerBase
{
    [HttpPost("[action]")]
    [AllowAnonymous] // yetki değişimi yapılacaktır
    public async Task<IActionResult> AddStock(AddStockCommandRequest request)
    {
        var result = await mediator.Send(request);
        return Ok(result);
    }
}