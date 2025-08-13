using ETicaret.Application.DTOs.Stocks.Request;
using ETicaret.Application.DTOs.Stocks.Results;

namespace ETicaret.Application.Abstractions;

public interface IStockService
{
    Task<AddStockResultDto> AddStockAsync(IEnumerable<AddStockDto> dtos);
    Task<bool> AddStockAsync(AddStockDto dto);
}