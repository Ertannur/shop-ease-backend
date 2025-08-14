using ETicaret.Application.CQRS.Commands.Basket;
using ETicaret.Application.CQRS.Queries.Basket;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,User")]
public class BasketController(IMediator mediator) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetBasketItems()
    {
        var result = await mediator.Send(new GetBasketItemsQuery());
        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest request)
    {
        var result = await mediator.Send(request);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest request)
    {
        await mediator.Send(request);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> DeleteBasketItem(RemoveBasketItemCommandRequest request)
    {
        await mediator.Send(request);
        return Ok();
    }
}