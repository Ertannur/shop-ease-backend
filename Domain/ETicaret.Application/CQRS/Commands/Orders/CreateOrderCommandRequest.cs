using ETicaret.Application.CQRS.Results.Orders;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Orders;

public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResult>
{
    public Guid AdressId { get; set; }
   
}