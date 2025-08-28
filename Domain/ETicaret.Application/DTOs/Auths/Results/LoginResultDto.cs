using ETicaret.Application.DTOs.Users.Results;

namespace ETicaret.Application.DTOs.Auths.Results;

public class LoginResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = String.Empty;
    public Token?  Token { get; set; } 
    public User?   User { get; set; }
}