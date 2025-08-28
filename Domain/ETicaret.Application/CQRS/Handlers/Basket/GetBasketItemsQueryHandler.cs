using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Queries.Basket;
using ETicaret.Application.CQRS.Results.Basket;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Basket;

public class GetBasketItemsQueryHandler(IBasketService basketService) : IRequestHandler<GetBasketItemsQuery,IEnumerable<GetBasketItemsQueryResult>>
{
    public async Task<IEnumerable<GetBasketItemsQueryResult>> Handle(GetBasketItemsQuery request, CancellationToken cancellationToken)
    {
        var basketItems = await basketService.GetBasketItemsAsync();
        return basketItems.Select(x=> new  GetBasketItemsQueryResult()
        {
            BasketItemId = x.BasketItemId,
            Quantity = x.Quantity,
            Price = x.Price,
            Name = x.Name,
            ImageUrl = x.ImageUrl
        });
    }
}