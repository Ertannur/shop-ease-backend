using ETicaret.Application.DTOs.Orders.Requests;
using ETicaret.Application.DTOs.Orders.Results;
using ETicaret.Domain.Entities;

namespace ETicaret.Application.Abstractions;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrderDto dto);
    Task<IEnumerable<ListCurrentUserOrdersResultDto>> ListCurrentUserOrdersAsync();
}