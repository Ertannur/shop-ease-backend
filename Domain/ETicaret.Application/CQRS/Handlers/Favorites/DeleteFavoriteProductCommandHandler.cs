using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Favorites;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Favorites;

public class DeleteFavoriteProductCommandHandler(IProductService productService) : IRequestHandler<DeleteFavoriteProductCommandRequest, bool>
{
    public async Task<bool> Handle(DeleteFavoriteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await productService.RemoveFavoritesAsync(request.ProductId);
        return result;
    }
}