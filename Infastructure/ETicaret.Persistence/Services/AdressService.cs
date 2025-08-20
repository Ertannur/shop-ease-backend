using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Adresses.Requests;
using ETicaret.Application.DTOs.Adresses.Results;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class AdressService(ETicaretDbContext context, IHttpContextAccessor httpContextAccessor)  : IAdressService
{
    private async Task<AppUser?> CurrentUser()
    {
        var userName = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        AppUser? appUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return appUser;
    }
    public async Task<AddAdressResultDto> AddAdressAsync(AddAdressDto dto)
    {
        var user = await CurrentUser();
        if (user == null)
            return new()
            {
                Success = false,
                Message = "Kullanıcı Bulunamadı"
            };
        Adress adress = new Adress()
        {
            Title = dto.Title,
            Name = dto.Name,
            Surname = dto.Surname,
            City = dto.City,
            Address = dto.Address,
            District = dto.District,
            PostCode = dto.PostCode,
            Email = dto.Email,
            Phone = dto.Phone,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false,
            UserId = user.Id,
        };
        await context.Adresses.AddAsync(adress);
        await context.SaveChangesAsync();
        return new()
        {
            Success = true,
            Message = "Adres Bilgisi Başarıyla Eklendi"
        };
    }

    public async Task<IEnumerable<GetUserAdressResultDto>> GetUserAdressAsync()
    {
        var user = await CurrentUser();
        if (user is null)
        {
            return new List<GetUserAdressResultDto>();
        }
        IEnumerable<Adress> adresses = await context.Adresses.Where(x => x.UserId == user.Id && x.IsDeleted == false).ToListAsync();
        return adresses.Select(x=> new GetUserAdressResultDto()
        {
            AdressId = x.Id,
            Title = x.Title,
            Name = x.Name,
            Surname = x.Surname,
            City = x.City,
            District = x.District,
            Address = x.Address,
            Email = x.Email,
            Phone = x.Phone,
            PostCode = x.PostCode
        });
    }
}