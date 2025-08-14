using ETicaret.Application.Abstractions;
using ETicaret.Application.Abstractions.Storage;
using ETicaret.Application.CQRS.Commands.Favorites;
using ETicaret.Application.CQRS.Commands.Products;
using ETicaret.Application.CQRS.Queries.Favorites;
using ETicaret.Application.CQRS.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController(IMediator mediator, IProductService productService) : ControllerBase
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
    [AllowAnonymous]
    public async Task<IActionResult> AddProduct(AddProductCommandRequest addProductCommandRequest)
    {
      
        var result=await mediator.Send(addProductCommandRequest);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
        
    }
    /*
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
    */
    [Authorize(Roles = "Admin, User")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetFavoriteProducts()
    {
        var result = await mediator.Send(new GetFavoritesProductQuery());
        return Ok(result);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPost("[action]")]
    public async Task<IActionResult> DeleteFavoriteProduct(DeleteFavoriteProductCommandRequest removeFavoriteProductCommandRequest)
    {
        var result = await mediator.Send(removeFavoriteProductCommandRequest);
        if(result)
            return Ok();
        return BadRequest(result);
    }
    [Authorize(Roles = "Admin, User")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddFavoriteProduct(AddFavoriteCommandRequest request)
    {
        var result = await mediator.Send(request);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }
}