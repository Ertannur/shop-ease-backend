using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Basket;
using ETicaret.Application.CQRS.Results.Basket;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Basket;

public class RemoveBasketItemCommandHandler(IBasketService basketService) : IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResult>
{
    public async Task<RemoveBasketItemCommandResult> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        var value = await basketService.RemoveBasketItemAsync(request.BasketItemId);
        return new()
        {
            Success = value
        };
    }
}
