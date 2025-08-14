namespace ETicaret.Application.CQRS.Results.Basket;

public class GetBasketItemsQueryResult
{
    public Guid BasketItemId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
}