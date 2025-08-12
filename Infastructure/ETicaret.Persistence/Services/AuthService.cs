using System.Text;
using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Auths.Requests;
using ETicaret.Application.DTOs.Auths.Results;
using ETicaret.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace ETicaret.Persistence.Services;

public class AuthService(UserManager<AppUser> userManager, IRoleService roleService, 
    SignInManager<AppUser> signInManager,ITokenService tokenService
    ,IEmailService emailService, IConfiguration configuration) : IAuthService
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

    public async Task<ForgotPasswordResultDto> ForgotPasswordAsync(string email)
    {
        var  user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return new()
            {
                Success = false,
                Message = "İlgili E Posta Adresi Sistemimizde Kayıtlı Değildir"
            };
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        byte [] tokenBytes = Encoding.UTF8.GetBytes(token);
        token = WebEncoders.Base64UrlEncode(tokenBytes); // bu şekil yapmazsan http üzerinden gönderirken hata yaşarsın
        string body =
            $"Şifre sıfırlamak için lütfen aşağıdaki linke tıklayınız. {configuration["Domain"]}/resetPassword?token={token}";
        await emailService.SendEmailAsync(user.Email, "Şifre Sıfırlama İsteği",body);
        return new()
        {
            Success = true,
            Message = "Şifre Sıfırlama Bağlantısı Başarıyla Gönderildi"
        };
    }

    public async Task<ResetPasswordResultDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user is null)
            return new()
            {
                Success = false,
                Message = "Kullanıcı Bilgisi Bulunamadı"
            };
        var tokenBytes  = WebEncoders.Base64UrlDecode(resetPasswordDto.Token);
        var token = Encoding.UTF8.GetString(tokenBytes);
        var result = await userManager.ResetPasswordAsync(user,token,resetPasswordDto.NewPassword);
        if (result.Succeeded)
            return new()
            {
                Success = result.Succeeded,
                Message = "Şifreniz Başarıyla Sıfırlanmıştır. Yeni Şifreniz İle Giriş Yapabilirsiniz"
            };
        return new()
        {
            Success = false,
            Message = result.Errors.First().Description
        };
    }
}