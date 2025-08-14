using ETicaret.Application.CQRS.Results.Basket;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Basket;

public class UpdateQuantityCommandRequest : IRequest<UpdateQuantityCommandResult>
{
    public Guid BasketItemId { get; set; }
    public int Quantity { get; set; }
}