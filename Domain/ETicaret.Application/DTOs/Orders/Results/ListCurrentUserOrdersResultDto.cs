using ETicaret.Application.DTOs.Products.Results;

namespace ETicaret.Application.DTOs.Orders.Results;

public class ListCurrentUserOrdersResultDto
{
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string? Address { get; set; }
    public IEnumerable<ProductResultDto> Products { get; set; }
}