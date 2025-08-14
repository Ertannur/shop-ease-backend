namespace ETicaret.Application.CQRS.Results.Favorites;

public class GetFavoritesProductQueryResult
{
    public Guid ProductId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}