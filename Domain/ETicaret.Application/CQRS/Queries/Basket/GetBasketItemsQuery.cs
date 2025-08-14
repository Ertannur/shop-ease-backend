using ETicaret.Application.CQRS.Results.Basket;
using MediatR;

namespace ETicaret.Application.CQRS.Queries.Basket;

public class GetBasketItemsQuery : IRequest<IEnumerable<GetBasketItemsQueryResult>>
{
    
}