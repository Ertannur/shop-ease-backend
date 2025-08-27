using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Users.Requests;
using ETicaret.Application.DTOs.Users.Results;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class UserService(UserManager<AppUser> userManager, ETicaretDbContext context, IHttpContextAccessor httpContextAccessor) : IUserService
{
    private async Task<AppUser?> CurrentUser()
    {
        var userName = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        AppUser? appUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return appUser;
    }
    public async Task<UpdateUserResultDto> UpdateUserAsync(UpdateUserDto dto)
    {
        var user = await userManager.FindByIdAsync(dto.Id.ToString());
        if (user == null)
            return new () { Success = false , Message = "Kullanıcı Bulunamadı" };
        var isCheck = await userManager.CheckPasswordAsync(user, dto.Password);
        if (!isCheck)
            return new() { Success = false, Message = "Şifre Bilgisi Yanlış" };
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;
        user.DateOfBirth = dto.DateOfBirth;
        user.Gender = dto.Gender;
        var result = await userManager.UpdateAsync(user);
        if(result.Succeeded)
            return new () { Success = true,Message = "Kullanıcı Bilgisi Başarıyla Güncellendi"};
        return new() { Success = false, Message = $"{result.Errors.FirstOrDefault()?.Description}" };
    }

    public async Task<GetUserByIdResultDto?> GetUserByIdAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return null;
        }

        return new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth.Value,
            Gender = user.Gender.Value,
            LastName = user.LastName
        };
    }

    public async Task<IEnumerable<GetUserResultDto>> GetUsersAsync()
    {
        IEnumerable<AppUser> users = await context.Users.OrderBy(p => p.FirstName).ToListAsync();
        return users.Select(x => new GetUserResultDto()
        {
            Id = x.Id.ToString(),
            FullName = x.FirstName + " " + x.LastName
        });
    }

    public async Task<IEnumerable<GetUserResultDto>> GetSupportAsync()
    {
        var user = await context.Users.Where(x=> x.FirstName == "Müşteri").OrderBy(p => p.FirstName).FirstOrDefaultAsync();
        GetUserResultDto result = new GetUserResultDto()
        {
            Id = user.Id.ToString(),
            FullName = user.FirstName + " " + user.LastName
        };
        var list = new List<GetUserResultDto>();
        list.Add(result);
        return list;
    }

    public async Task<UserChangePasswordResultDto> ChangePasswordAsync(UserChangePasswordDto dto)
    {
        var user = await CurrentUser();
        if(user is null)
            return new() {Success = false, Message = "Kullanıcı Bulunamadı"};
        var isCheck = await userManager.CheckPasswordAsync(user, dto.OldPassword);
        if (!isCheck)
            return new() { Success = false, Message = "Şu Anki Şifre Bilgisi Yanlış" };
        var result = await userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
        if (result.Succeeded)
            return new(){Success = true, Message = "Şifreniz Başarıyla Güncellendi"};
        return new() { Success = false, Message = $"Şifreniz Güncellenirken Bir Hata Meydana Geldi" };
    }

    public async Task<AppUser?> FindUserByIdAsync(Guid userId)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate)
    {
        if (user != null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = accessTokenDate.AddDays(1);
            await userManager.UpdateAsync(user);
        }
    }
}