using ETicaret.Application.CQRS.Commands.Orders;
using ETicaret.Application.CQRS.Queries.Orders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ETicaret.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,User")]
public class OrderController(IMediator mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandResult)
    {
        await mediator.Send(createOrderCommandResult);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ListCurrentUserOrders()
    {
        var result = await mediator.Send(new GetCurrentUserOrdersQuery());
        return Ok(result);
    }
    
    
}