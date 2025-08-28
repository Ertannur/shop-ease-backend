using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Baskets.Requests;
using ETicaret.Application.DTOs.Baskets.Results;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager,ETicaretDbContext context) : IBasketService
{
    private async Task<Basket?> ContextUser()
    {
        var userName = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(userName))
        {
            AppUser? appUser = await userManager.Users.Include(x => x.Baskets)
                .FirstOrDefaultAsync(x => x.UserName == userName);
            var _basket = from basket in appUser.Baskets
                join order in context.Set<Order>()
                    on basket.Id equals order.Id into BasketOrders
                from order in BasketOrders.DefaultIfEmpty()
                select new
                {
                    Basket = basket,
                    Order = order
                };
            Basket? targetBasket = null;
            if (_basket.Any(b => b.Order is null))
                targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
            else
            {
                targetBasket = new();
                appUser.Baskets.Add(targetBasket);
            }
               
            await context.SaveChangesAsync();
            return targetBasket;
        }
        return null;
    }
    public async Task<IEnumerable<GetBasketItemResultDto>> GetBasketItemsAsync()
    {
        Basket? basket = await ContextUser();
        if(basket is null)
            return new List<GetBasketItemResultDto>();
        /*Basket? result = await context.Set<Basket>().Include(x=>x.BasketItems)
            .ThenInclude(x=> x.Product)
            .ThenInclude(x=> x.Images)
            .FirstOrDefaultAsync(x=> x.Id == basket.Id);
        return result.BasketItems;
        */
        var result = context.BasketItems
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.BasketId == basket.Id)
            .Select(x => new GetBasketItemResultDto
            {
                BasketItemId = x.Id,
                ImageUrl = x.Product.Images.FirstOrDefault(a => a.ProductId == x.ProductId).ImageUrl,
                Name = x.Product.Name,
                Price = x.Product.Price,
                Quantity = x.Quantity
            });
        return result;
    }

    public async Task AddItemToBasketAsync(AddItemToBasketDto addItemToBasketDto)
    {
       Basket? basket = await ContextUser();
       if (basket != null)
       {
           BasketItem? basketItem =  await context.BasketItems.FirstOrDefaultAsync(x => x.BasketId == basket.Id && x.ProductId == addItemToBasketDto.ProductId);
           if (basketItem != null)
               basketItem.Quantity++;
           else
               await context.BasketItems.AddAsync(new()
               {
                   BasketId = basket.Id,
                   ProductId = addItemToBasketDto.ProductId,
                   Quantity = addItemToBasketDto.Quantity,
               });
           await context.SaveChangesAsync();
       }
    }

    public async Task<bool> UpdateQuantityAsync(UpdateBasketItemDto updateBasketItemDto)
    {
        BasketItem? basketItem = await context.BasketItems.FirstOrDefaultAsync(x => x.Id == updateBasketItemDto.BasketItemId);
        if (basketItem != null)
        {
            basketItem.Quantity = updateBasketItemDto.Quantity;
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> RemoveBasketItemAsync(Guid basketItemId)
    {
        BasketItem? basketItem = await context.BasketItems.FirstOrDefaultAsync(x => x.Id == basketItemId);
        if (basketItem != null)
        {
            context.Remove(basketItem);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<Basket?> GetUserActiveBasketAsync()
    {
        Basket? basket = await ContextUser();
        return basket;
    }
}