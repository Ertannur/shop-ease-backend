using ETicaret.Application.CQRS.Results.Orders;
using MediatR;

namespace ETicaret.Application.CQRS.Queries.Orders;

public class GetCurrentUserOrdersQuery : IRequest<IEnumerable<GetCurrentUserOrdersQueryResult>>
{
    
}