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
            
            var result = await _productService.AddFavoritesAsync(request.ProductId);

                return new AddFavoritesCommandResult
                {
                    Success = result.Success,
                    Message = result.Message ,
                    ProductId = result.ProductId
                };
          
        }
    }
}
