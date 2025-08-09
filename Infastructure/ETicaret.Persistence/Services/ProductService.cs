using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Products.Results;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Paggination;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class ProductService(ETicaretDbContext context) : IProductService
{
    public async Task<GetProductResultDto> GetProducts(string? category, int currentPage = 1, int pageSize = 8)
    {
        var products = context.Products.Include(x => x.Category).Include(x => x.ProductTypes)
            .Where(x=> x.IsDeleted == false);
        if (!string.IsNullOrWhiteSpace(category))
        {
            products = products.Where(p=> p.Category.Name == category );
        }

        var paginationResponse = await products.Where(new PaginationRequest(currentPage, pageSize));
        var productViewModels = paginationResponse.Data.Select(x => new ProductViewModel()
        {
            Name = x.Name,
            ProductId = x.Id,
            ImageUrl = x.ImageUrl,
            Price = x.Price,
        });
        return new()
        {
            Products = productViewModels,
            HasNextPage = paginationResponse.HasNextPage,
            HasPreviousPage = paginationResponse.HasPreviousPage,
            TotalPage = paginationResponse.TotalPages,
            TotalCount = paginationResponse.TotalCount
        };
    }
}