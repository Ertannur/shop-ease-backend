using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ETicaret.Application.Abstractions;
using ETicaret.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ETicaret.Infastructure.Services.Token;

public class TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager, IRoleService roleService) : ITokenService
{
    public async Task<Application.DTOs.Token> CreateAccessTokenAsync(string userId)
    {
        Application.DTOs.Token token = new();
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        token.Expiration = DateTime.UtcNow.AddDays(5);
        var roleClaims = await roleService.GetRoleClaimsAsync(userId);
        AppUser? user = await userManager.FindByIdAsync(userId);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            user != null ? new Claim(ClaimTypes.Name, user.Email) : null
        }.Union(roleClaims);
        JwtSecurityToken securityToken = new(
            audience: configuration["JWT:Audience"],
            issuer: configuration["JWT:Issuer"],
            expires: token.Expiration,
            signingCredentials: signingCredentials,
            notBefore: DateTime.UtcNow,
            claims: claims);
        JwtSecurityTokenHandler jwtTokenHandler = new();
        token.AccessToken = jwtTokenHandler.WriteToken(securityToken);
        return token;
    }
}