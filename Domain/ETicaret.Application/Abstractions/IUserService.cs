using ETicaret.Application.DTOs.Users.Requests;
using ETicaret.Application.DTOs.Users.Results;

namespace ETicaret.Application.Abstractions;

public interface IUserService
{
    Task<UpdateUserResultDto> UpdateUserAsync(UpdateUserDto dto);
    Task<GetUserByIdResultDto?>  GetUserByIdAsync(Guid userId);
    Task<UserChangePasswordResultDto> ChangePasswordAsync(UserChangePasswordDto dto);
}