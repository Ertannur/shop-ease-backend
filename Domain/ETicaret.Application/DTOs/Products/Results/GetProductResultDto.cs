using ETicaret.Domain.Entities;

namespace ETicaret.Application.DTOs.Products.Results;

public class GetProductResultDto
{
    public IEnumerable<ProductViewModel> Products { get; set; }
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}
public class ProductViewModel
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}