namespace ETicaret.Application.DTOs.Baskets.Requests;

public class UpdateBasketItemDto
{
    public Guid BasketItemId { get; set; }
    public int Quantity { get; set; }
}