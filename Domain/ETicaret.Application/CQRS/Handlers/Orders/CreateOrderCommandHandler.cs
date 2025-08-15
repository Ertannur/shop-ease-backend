using ETicaret.Application.Abstractions;
using ETicaret.Application.Configurations;
using ETicaret.Application.CQRS.Commands.Orders;
using ETicaret.Application.CQRS.Results.Orders;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Orders;

public class CreateOrderCommandHandler(IOrderService orderService) : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResult>
{
    public async Task<CreateOrderCommandResult> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var crateOrderDto = ModelMapper.MapCreateOrderDto(request);
        await orderService.CreateOrderAsync(crateOrderDto);
        return new();
    }
}