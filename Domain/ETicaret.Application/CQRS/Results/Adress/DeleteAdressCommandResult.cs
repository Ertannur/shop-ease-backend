namespace ETicaret.Application.CQRS.Results.Adress;

public class DeleteAdressCommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}