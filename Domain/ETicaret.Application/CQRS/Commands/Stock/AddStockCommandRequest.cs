using ETicaret.Application.CQRS.Results.Stocks;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Stock;

public class AddStockCommandRequest : IRequest<AddStockCommandResult>
{
   public IEnumerable<AddStockViewModel> AddStock {get; set;}
}

public class AddStockViewModel()
{
    public Guid ProductTypeId { get; set; }
    public int Quantity { get; set; }
}