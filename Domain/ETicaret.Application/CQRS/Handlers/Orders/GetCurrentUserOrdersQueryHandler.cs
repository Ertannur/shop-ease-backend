using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Queries.Orders;
using ETicaret.Application.CQRS.Results.Orders;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Orders;

public class GetCurrentUserOrdersQueryHandler(IOrderService orderService) :  IRequestHandler<GetCurrentUserOrdersQuery, IEnumerable<GetCurrentUserOrdersQueryResult>>
{
    public async Task<IEnumerable<GetCurrentUserOrdersQueryResult>> Handle(GetCurrentUserOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = await orderService.ListCurrentUserOrdersAsync();
        return result.Select(x => new GetCurrentUserOrdersQueryResult
        {
            OrderId = x.OrderId,
            OrderDate = x.OrderDate,
            Address = x.Address,
            Products = x.Products
        });
    }
}