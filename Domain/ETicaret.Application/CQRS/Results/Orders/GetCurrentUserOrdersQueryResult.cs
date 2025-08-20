using ETicaret.Application.DTOs.Products.Results;

namespace ETicaret.Application.CQRS.Results.Orders;

public class GetCurrentUserOrdersQueryResult
{
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string? Address { get; set; }
    public IEnumerable<ProductResultDto> Products { get; set; }
}