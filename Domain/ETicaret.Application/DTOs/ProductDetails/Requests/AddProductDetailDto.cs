
using ETicaret.Domain.Enums;

namespace ETicaret.Application.DTOs.ProductDetails.Requests;

public class AddProductDetailDto
{
    public Guid ProductId { get; set; }
    public ColorType Color { get; set; }
    public string Size { get; set; }
    public int StockQuantity { get; set; }
}