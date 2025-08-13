using ETicaret.Application.Abstractions;
using ETicaret.Application.Abstractions.Storage.Azure;
using ETicaret.Application.DTOs.Images.Requests;
using ETicaret.Application.DTOs.Products.Results;
using ETicaret.Application.DTOs.Products.Requests;
using ETicaret.Domain.Entities;
using ETicaret.Domain.Enums;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Paggination;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class ProductService(ETicaretDbContext context, IImageService imageService) : IProductService
{
    public async Task<GetProductResultDto> GetProductsAsync(string? category, int currentPage = 1, int pageSize = 8)
    {
        var products = context.Products.Include(x => x.Category)
            .Include(x => x.ProductTypes)
            .Include(x => x.Images)
            .AsSplitQuery()
            .Where(x => x.IsDeleted == false);

        if (!string.IsNullOrWhiteSpace(category))
        {
            products = products.Where(p => p.Category.Name == category);
        }

        var paginationResponse = await products.Where(new PaginationRequest(currentPage, pageSize), x => x.CreatedDate);
        var productViewModels = paginationResponse.Data.Select(x => new ProductViewModel()
        {
            Name = x.Name,
            ProductId = x.Id,
            Price = x.Price,
            ImageUrl = x.Images.Select(x => x.ImageUrl).FirstOrDefault(),
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

    public async Task<GetProductByIdResultDto> GetProductByIdAsync(Guid id)
    {
        var colorTypeEnum = typeof(ColorType);
        var productDto = await context.Products
            .AsSplitQuery()
            .Where(p => p.Id == id)
            .Select(p => new GetProductByIdResultDto
            {
                ProductId = p.Id.ToString(),
                Title = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.ProductTypes
                    .Where(pt => pt.Stock != null)
                    .Sum(pt => (int?)pt.Stock.Quantity) ?? 0,
                Images = p.Images
                    .Select(i => i.ImageUrl)
                    .ToList(),
                Details = p.ProductTypes
                    .Select(pt => new ProductDetail
                    {
                        Color = pt.Color != null
                            ? pt.Color.ColorType.ToString()
                            : null,
                        Size = pt.Size,
                        Stock = pt.Stock != null
                            ? pt.Stock.Quantity
                            : 0
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (productDto == null)
        {
            throw new Exception("Ürün bulunamadı.");
        }

        return productDto;
    }

    //eklendi
    public async Task<AddProductResultDto> AddProductAsync(AddProductRequest dto)
    {
        Guid productId = Guid.NewGuid();
        var product = new Product
        {
            Id = productId,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        };
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return new AddProductResultDto() {Message="Ürün eklendi",Success=true, ProductId = productId};
    }

    
}
