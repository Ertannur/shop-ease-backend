using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Orders.Requests;
using ETicaret.Application.DTOs.Orders.Results;
using ETicaret.Application.DTOs.Products.Results;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class OrderService(ETicaretDbContext context, IBasketService basketService, IHttpContextAccessor httpContextAccessor) : IOrderService
{
    private async Task<AppUser?> CurrentUser()
    {
        var userName = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        AppUser? appUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return appUser;
    }
    public async Task CreateOrderAsync(CreateOrderDto dto)
    {
        var basket = await basketService.GetUserActiveBasketAsync();
        await context.Order.AddAsync(new()
        {
            AdressId = dto.AdressId,
            Id = basket.Id,
            IsDeleted = false,
        });
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ListCurrentUserOrdersResultDto>> ListCurrentUserOrdersAsync()
    {
        var currentUser = await CurrentUser();
        if (currentUser is null)
        {
            return  new List<ListCurrentUserOrdersResultDto>();
        }
        var value = context.Order.Where(X=> X.Basket.UserId == currentUser.Id && X.IsDeleted == false).Select(x => new ListCurrentUserOrdersResultDto()
        {
            OrderId = x.Id,
            OrderDate = x.CreatedDate,
            Address = x.Adress.Address + " " + x.Adress.City + " " + x.Adress.District + " " +x.Adress.PostCode,
            Products = x.Basket.BasketItems
                .Select(p =>  new ProductResultDto()
                {
                    ProductId = p.ProductId,
                    TotalPrice = p.Quantity*p.Product.Price,
                    ProductName = p.Product.Name,
                    Quantity = p.Quantity
                })
        });
        return value;
    }
}