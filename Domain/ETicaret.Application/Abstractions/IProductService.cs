using ETicaret.Application.DTOs.Products.Requests;
using ETicaret.Application.DTOs.Products.Results;

namespace ETicaret.Application.Abstractions;

public interface IProductService
{
    Task<GetProductResultDto> GetProductsAsync(string? category,int currentPage = 1, int pageSize = 8);
    Task<GetProductByIdResultDto> GetProductByIdAsync(Guid id);
    Task<AddProductResultDto> AddProductAsync(AddProductRequest dto);
    Task<bool> AddFavoritesAsync(Guid appUserId, Guid productId);

}