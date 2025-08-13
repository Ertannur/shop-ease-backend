using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Stocks.Request;
using ETicaret.Application.DTOs.Stocks.Results;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;

namespace ETicaret.Persistence.Services;

public class StockService(ETicaretDbContext context) : IStockService
{
    public async Task<AddStockResultDto> AddStockAsync(IEnumerable<AddStockDto> dtos)
    {
        IEnumerable<Stock> stocks = dtos.Select(x => new Stock
        {
            Id = Guid.NewGuid(),
            ProductTypeId = x.ProductTypeId,
            Quantity = x.Quantity,
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow,
        });
        await context.Stocks.AddRangeAsync(stocks);
        await context.SaveChangesAsync();
        return new()
        {
            Success = true,
            Message = "Stock Başarıyla Eklendi"
        };
    }

    public async Task<bool> AddStockAsync(AddStockDto dto)
    {
        Stock stock = new()
        {
            ProductTypeId = dto.ProductTypeId,
            Quantity = dto.Quantity,
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow,
        };
        await context.Stocks.AddAsync(stock);
        await context.SaveChangesAsync();
        return true;
    }
}