using ETicaret.Application.DTOs.Products.Results;
using ETicaret.Domain.Enums;

namespace ETicaret.Application.CQRS.Results.Products;

public class GetProductByIdQueryResult
{
    public string ProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public IEnumerable<string> Images { get; set; }
    public IEnumerable<ProductDetail>  Details { get; set; }
}