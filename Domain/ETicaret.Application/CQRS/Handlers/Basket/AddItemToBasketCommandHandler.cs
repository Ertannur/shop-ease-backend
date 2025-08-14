using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Basket;
using ETicaret.Application.CQRS.Results.Basket;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Basket;

public class AddItemToBasketCommandHandler(IBasketService basketService) : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResult>
{
    public async Task<AddItemToBasketCommandResult> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
    {
        await basketService.AddItemToBasketAsync(new()
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity
        });
        return new();
    }
}