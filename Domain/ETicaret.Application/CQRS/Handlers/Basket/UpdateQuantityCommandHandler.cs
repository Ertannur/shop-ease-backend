using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Basket;
using ETicaret.Application.CQRS.Results.Basket;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Basket;

public class UpdateQuantityCommandHandler(IBasketService basketService) : IRequestHandler<UpdateQuantityCommandRequest, UpdateQuantityCommandResult>
{
    public async Task<UpdateQuantityCommandResult> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await basketService.UpdateQuantityAsync(new()
        {
            BasketItemId = request.BasketItemId,
            Quantity = request.Quantity
        });
        return new()
        {
            Success = result
        };
    }
}