using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.ProductDetails.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductDetailController(IProductDetailService productDetailService) : ControllerBase
{
    [HttpPost("[action]")]
    [AllowAnonymous] // yetki değişimi yapılacaktır
    public async Task<IActionResult> AddProductDetail(IEnumerable<AddProductDetailDto> dto)
    {
        //await productDetailService.AddProductDetail(dto);
        return Ok();   
    }
}