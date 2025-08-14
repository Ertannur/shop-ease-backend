using ETicaret.Application.CQRS.Results.Favorites;
using MediatR;

namespace ETicaret.Application.CQRS.Queries.Favorites;

public class GetFavoritesProductQuery : IRequest<IEnumerable<GetFavoritesProductQueryResult>>
{
    
}