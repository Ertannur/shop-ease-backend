using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Favorites;
using ETicaret.Application.CQRS.Results.Favorites;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaret.Application.CQRS.Handlers.Favorites
{
    public class AddFavoritesCommandHandler : IRequestHandler<AddFavoriteCommandRequest, AddFavoritesCommandResult>
    {
        private readonly IProductService _productService;

        public AddFavoritesCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<AddFavoritesCommandResult> Handle(AddFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            // Servis çağrısı
            var result = await _productService.AddFavoritesAsync(request.UserId, request.ProductId);

            if (result) // Favori eklendiyse
            {
                return new AddFavoritesCommandResult
                {
                    Success = true,
                    Message = "Ürün favorilere eklendi.",
                    ProductId = request.ProductId
                };
            }
            else // Eklenemediyse
            {
                return new AddFavoritesCommandResult
                {
                    Success = false,
                    Message = "Ürün zaten favorilerde veya eklenirken hata oluştu.",
                    ProductId = request.ProductId
                };
            }
        }
    }
}
