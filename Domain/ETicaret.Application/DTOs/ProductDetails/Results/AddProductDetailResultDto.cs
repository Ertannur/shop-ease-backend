namespace ETicaret.Application.DTOs.ProductDetails.Results;

public class AddProductDetailResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<Guid> ProductTypeIds { get; set; }
}