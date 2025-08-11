using ETicaret.Domain.Enums;

namespace ETicaret.Application.DTOs.Products.Results;

public class GetProductByIdResultDto
{
    public string ProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public IEnumerable<string> Images { get; set; }
    public IEnumerable<string> Sizes { get; set; }
    public IEnumerable<string> Colors { get; set; }
}