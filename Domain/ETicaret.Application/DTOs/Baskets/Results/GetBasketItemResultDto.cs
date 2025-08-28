namespace ETicaret.Application.DTOs.Baskets.Results;

public class GetBasketItemResultDto
{
    public Guid BasketItemId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
}