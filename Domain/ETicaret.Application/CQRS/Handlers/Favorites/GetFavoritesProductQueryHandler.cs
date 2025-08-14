using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Queries.Favorites;
using ETicaret.Application.CQRS.Results.Favorites;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Favorites;

public class GetFavoritesProductQueryHandler(IProductService productService) : IRequestHandler<GetFavoritesProductQuery, IEnumerable<GetFavoritesProductQueryResult>>
{
    public async Task<IEnumerable<GetFavoritesProductQueryResult>> Handle(GetFavoritesProductQuery request, CancellationToken cancellationToken)
    {
        var result = await productService.GetFavoritesAsync();
        return result.Select(x=> new GetFavoritesProductQueryResult()
        {
            ProductId = x.ProductId,
            Title = x.Title,
            Price = x.Price,
            ImageUrl = x.ImageUrl
        });
    }
}