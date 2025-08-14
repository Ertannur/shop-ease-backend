using ETicaret.Application.CQRS.Results.Basket;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Basket;

public class RemoveBasketItemCommandRequest : IRequest<RemoveBasketItemCommandResult>
{
    public Guid BasketItemId { get; set; }
}