using ETicaret.Application.DTOs.Users.Requests;
using ETicaret.Application.DTOs.Users.Results;
using ETicaret.Domain.Entities;

namespace ETicaret.Application.Abstractions;

public interface IUserService
{
    Task<UpdateUserResultDto> UpdateUserAsync(UpdateUserDto dto);
    Task<GetUserByIdResultDto?>  GetUserByIdAsync(Guid userId);
    Task<IEnumerable<GetUserResultDto>> GetUsersAsync();
    Task<IEnumerable<GetUserResultDto>> GetSupportAsync();
    Task<UserChangePasswordResultDto> ChangePasswordAsync(UserChangePasswordDto dto);
    Task<AppUser?> FindUserByIdAsync(Guid userId);
    Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate);
}