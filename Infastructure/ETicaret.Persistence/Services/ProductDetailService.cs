using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.ProductDetails.Requests;
using ETicaret.Application.DTOs.ProductDetails.Results;
using ETicaret.Application.DTOs.Stocks.Request;
using ETicaret.Domain.Entities;
using ETicaret.Domain.Enums;
using ETicaret.Persistence.Contexts;

namespace ETicaret.Persistence.Services;

public class ProductDetailService(ETicaretDbContext context, IStockService stockService) : IProductDetailService
{
    public async Task<AddProductDetailResultDto> AddProductDetail(IEnumerable<AddProductDetailDto> dtos)
    {
        List<ProductType> productTypes = new List<ProductType>();
        List<Guid> productTypeIds = new List<Guid>();
        foreach (var dto in dtos)
        {
            var productTypeId = Guid.NewGuid();
            ProductType productType = new ProductType()
            {
                Id = productTypeId,
                ProductId = dto.ProductId,
                Size = dto.Size,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };
            await context.ProductTypes.AddAsync(productType);
            await context.SaveChangesAsync();
            await stockService.AddStockAsync(new AddStockDto(){ProductTypeId = productTypeId, Quantity = dto.StockQuantity});
            productTypeIds.Add(productTypeId);
        }
        return new()
        {
            ProductTypeIds = productTypeIds,
        };
    }
}