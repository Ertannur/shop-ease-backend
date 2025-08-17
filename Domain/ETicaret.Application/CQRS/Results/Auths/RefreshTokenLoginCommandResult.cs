using ETicaret.Application.DTOs;
using MediatR;

namespace ETicaret.Application.CQRS.Results.Auths;

public class RefreshTokenLoginCommandResult 
{
    public bool Success { get; set; }
    public Guid? UserId { get; set; }
    public string Message { get; set; }
    public Token?  Token { get; set; } 
}