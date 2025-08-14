namespace ETicaret.Application.DTOs.Baskets.Requests;

public class AddItemToBasketDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}