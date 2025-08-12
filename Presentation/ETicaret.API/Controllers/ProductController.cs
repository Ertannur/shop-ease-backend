using ETicaret.Application.Abstractions.Storage;
using ETicaret.Application.CQRS.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController(IMediator mediator, IStorageService storageService) : ControllerBase
{
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProducts([FromQuery] int currentPage, [FromQuery] int pageSize, [FromQuery] string? category)
    {
        var getProductsQuery = new GetProductsQuery() { CurrentPage = currentPage, PageSize = pageSize, Category = category };
        var result = await mediator.Send(getProductsQuery);
        return Ok(result);      
    }

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductById([FromQuery] Guid id)
    {
        var result = await mediator.Send(new  GetProductByIdQuery() { Id = id });
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

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Upload(Guid id)
    {
        var result = await storageService.UploadAsync("photo-images", Request.Form.Files);
        return Ok();
    }
}