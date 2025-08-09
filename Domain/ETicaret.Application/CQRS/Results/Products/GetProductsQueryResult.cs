using ETicaret.Application.DTOs.Products.Results;

namespace ETicaret.Application.CQRS.Results.Products;

public class GetProductsQueryResult
{
    public IEnumerable<ProductViewModel> Products { get; set; }
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}