namespace ETicaret.Application.DTOs.Images.Results;

public class AddImageResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Guid ProductId { get; set; }
}