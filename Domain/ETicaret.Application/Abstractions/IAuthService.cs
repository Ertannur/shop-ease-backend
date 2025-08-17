using ETicaret.Application.DTOs.Auths.Requests;
using ETicaret.Application.DTOs.Auths.Results;

namespace ETicaret.Application.Abstractions;

public interface IAuthService
{
    Task<RegisterResultDto> RegisterAsync(RegisterDto registerDto);
    Task<LoginResultDto> LoginAsync(LoginDto loginDto);
    Task<LoginResultDto> RefreshTokenLoginAsync(string refreshToken);
    Task<ForgotPasswordResultDto> ForgotPasswordAsync(string email);
    Task<ResetPasswordResultDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
}