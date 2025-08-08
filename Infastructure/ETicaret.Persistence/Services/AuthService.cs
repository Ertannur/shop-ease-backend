using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Auths.Requests;
using ETicaret.Application.DTOs.Auths.Results;
using ETicaret.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Persistence.Services;

public class AuthService(UserManager<AppUser> userManager, IRoleService roleService, SignInManager<AppUser> signInManager,ITokenService tokenService) : IAuthService
{
    public async Task<RegisterResultDto> RegisterAsync(RegisterDto registerDto)
    {
        AppUser user = new()
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            DateOfBirth = registerDto.DateOfBirth,
            Gender = registerDto.Gender,
        };
        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded)
        {
            await roleService.AssignRoleToUserAsync(user,"User");
            return new()
            {
                Success = true,
                Message = "Kullanıcı Başarıyla Oluşturuldu"
            };
        }

        return new()
        {
            Success = false,
            Message = result.Errors.First().Description
        };
    }

    public async Task<LoginResultDto> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return new()
            {
                Success = false,
                Message = "Kullanıcı Bulunamadı"
            };
        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (signInResult.Succeeded)
        {
            await signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);
            var token = await tokenService.CreateAccessTokenAsync(user.Id.ToString());
            return new()
            {
                Success = true,
                Message = "Giriş İşlemi Başarıyla Gerçekleştirilmiştir",
                Token = token,
                UserId = user.Id
            };
        }

        return new()
        {
            Success = false,
            Message = "E Posta Adresiniz veya Şifreniz Hatalı"
        };
    }
}