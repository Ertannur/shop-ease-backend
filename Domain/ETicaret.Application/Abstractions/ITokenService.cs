using ETicaret.Application.DTOs;

namespace ETicaret.Application.Abstractions;

public interface ITokenService
{
    Task<Token> CreateAccessTokenAsync(string userId);
    
}