using ETicaret.Application.DTOs.Baskets.Requests;
using ETicaret.Application.DTOs.Baskets.Results;
using ETicaret.Domain.Entities;

namespace ETicaret.Application.Abstractions;

public interface IBasketService
{
    Task<IEnumerable<GetBasketItemResultDto>> GetBasketItemsAsync();
    Task AddItemToBasketAsync(AddItemToBasketDto addItemToBasketDto);
    Task<bool> UpdateQuantityAsync(UpdateBasketItemDto updateBasketItemDto);
    Task<bool> RemoveBasketItemAsync(Guid basketItemId);
    Task<Basket?> GetUserActiveBasketAsync();
}