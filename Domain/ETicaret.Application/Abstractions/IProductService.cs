using ETicaret.Application.DTOs.Products.Results;

namespace ETicaret.Application.Abstractions;

public interface IProductService
{
    Task<GetProductResultDto> GetProducts(string? category,int currentPage = 1, int pageSize = 8);
}