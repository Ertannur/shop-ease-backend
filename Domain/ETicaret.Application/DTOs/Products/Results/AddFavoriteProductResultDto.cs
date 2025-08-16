namespace ETicaret.Application.DTOs.Products.Results;

public class AddFavoriteProductResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Guid? ProductId { get; set; }
}