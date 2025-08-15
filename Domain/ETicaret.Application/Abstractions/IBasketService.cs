using ETicaret.Application.DTOs.Baskets.Requests;
using ETicaret.Domain.Entities;

namespace ETicaret.Application.Abstractions;

public interface IBasketService
{
    Task<IEnumerable<BasketItem>> GetBasketItemsAsync();
    Task AddItemToBasketAsync(AddItemToBasketDto addItemToBasketDto);
    Task UpdateQuantityAsync(UpdateBasketItemDto updateBasketItemDto);
    Task RemoveBasketItemAsync(Guid basketItemId);
    Task<Basket?> GetUserActiveBasketAsync();
}