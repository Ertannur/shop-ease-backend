using ETicaret.Application.CQRS.Results.Basket;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Basket;

public class AddItemToBasketCommandRequest : IRequest<AddItemToBasketCommandResult>
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}