using ETicaret.Application.DTOs;
using ETicaret.Application.DTOs.Users.Results;

namespace ETicaret.Application.CQRS.Results.Auths;

public class LoginUserCommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = String.Empty;
    public Token?  Token { get; set; } 
    public User?   User { get; set; }
}