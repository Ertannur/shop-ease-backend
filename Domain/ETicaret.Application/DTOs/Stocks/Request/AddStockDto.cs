namespace ETicaret.Application.DTOs.Stocks.Request;

public class AddStockDto
{
    public Guid ProductTypeId { get; set; }
    public int Quantity { get; set; }
}