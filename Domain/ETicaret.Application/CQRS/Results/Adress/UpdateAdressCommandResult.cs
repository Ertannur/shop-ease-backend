namespace ETicaret.Application.CQRS.Results.Adress;

public class UpdateAdressCommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; } =  string.Empty;
}