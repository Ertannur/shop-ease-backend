using ETicaret.Application.DTOs.Users.Requests;
using ETicaret.Application.DTOs.Users.Results;
using ETicaret.Domain.Entities;

namespace ETicaret.Application.Abstractions;

public interface IUserService
{
    Task<UpdateUserResultDto> UpdateUserAsync(UpdateUserDto dto);
    Task<GetUserByIdResultDto?>  GetUserByIdAsync(Guid userId);
    Task<UserChangePasswordResultDto> ChangePasswordAsync(UserChangePasswordDto dto);
    Task<AppUser?> FindUserByIdAsync(Guid userId);
}