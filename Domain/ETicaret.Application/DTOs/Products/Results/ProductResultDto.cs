namespace ETicaret.Application.DTOs.Products.Results;

public class ProductResultDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}