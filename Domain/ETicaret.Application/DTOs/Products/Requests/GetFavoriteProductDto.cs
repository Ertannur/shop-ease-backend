namespace ETicaret.Application.DTOs.Products.Requests;

public class GetFavoriteProductDto
{
    public Guid ProductId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}