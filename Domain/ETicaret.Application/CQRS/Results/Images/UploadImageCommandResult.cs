namespace ETicaret.Application.CQRS.Results.Images;

public class UploadImageCommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Guid ProductId { get; set; }
}