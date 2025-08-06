using ETicaret.Application.DTOs.Auths.Requests;
using ETicaret.Application.DTOs.Auths.Results;

namespace ETicaret.Application.Abstractions;

public interface IAuthService
{
    Task<RegisterResultDto> RegisterAsync(RegisterDto registerDto);
    Task<LoginResultDto> LoginAsync(LoginDto loginDto);
}