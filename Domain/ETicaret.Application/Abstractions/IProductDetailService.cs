using ETicaret.Application.DTOs.ProductDetails.Requests;
using ETicaret.Application.DTOs.ProductDetails.Results;

namespace ETicaret.Application.Abstractions;

public interface IProductDetailService
{
    Task<AddProductDetailResultDto> AddProductDetail(IEnumerable<AddProductDetailDto> dtos);
}