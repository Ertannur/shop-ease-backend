using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet("[action]")]
    [AllowAnonymous]
    // Silinmemiş olan productlar getirilecektir ve kategorilere göre
    public async Task<IActionResult> GetProducts([FromQuery] int currentPage, [FromQuery] int pageSize, [FromQuery] string? category)
    {
        var getProductsQuery = new GetProductsQuery() { CurrentPage = currentPage, PageSize = pageSize, Category = category };
        var result = await mediator.Send(getProductsQuery);
        return Ok(result);      
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddProduct()
    {
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct()
    {
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct()
    {
        return Ok();
    }   
}