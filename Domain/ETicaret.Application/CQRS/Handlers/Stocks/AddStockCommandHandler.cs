using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Stock;
using ETicaret.Application.CQRS.Results.Stocks;
using ETicaret.Application.DTOs.Stocks.Request;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Stocks;

public class AddStockCommandHandler(IStockService stockService) : IRequestHandler<AddStockCommandRequest, AddStockCommandResult>
{
    public async Task<AddStockCommandResult> Handle(AddStockCommandRequest request, CancellationToken cancellationToken)
    {
        var addStockDto = request.AddStock.Select(x => new AddStockDto()
        {
            ProductTypeId = x.ProductTypeId,
            Quantity = x.Quantity
        });
        var result = await stockService.AddStockAsync(addStockDto);
        return new()
        {
            Message = result.Message,
            Success = result.Success,
        };
    }
}