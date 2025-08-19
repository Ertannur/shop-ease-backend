using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Orders.Requests;
using ETicaret.Persistence.Contexts;

namespace ETicaret.Persistence.Services;

public class OrderService(ETicaretDbContext context, IBasketService basketService) : IOrderService
{
    public async Task CreateOrderAsync(CreateOrderDto dto)
    {
        var basket = await basketService.GetUserActiveBasketAsync();
        if(basket is not null)
            foreach (var item in basket.BasketItems)
            {
                
            }

        await context.Order.AddAsync(new()
        {
            AdressId = dto.AdressId,
            Id = basket.Id,
            IsDeleted = false,
        });
        await context.SaveChangesAsync();
    }
}