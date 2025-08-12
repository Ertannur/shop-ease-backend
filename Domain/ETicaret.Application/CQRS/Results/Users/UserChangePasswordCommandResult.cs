namespace ETicaret.Application.CQRS.Results.Users;

public class UserChangePasswordCommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
}