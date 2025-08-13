using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Adresses.Requests;
using ETicaret.Application.DTOs.Adresses.Results;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class AdressService(ETicaretDbContext context)  : IAdressService
{
    public async Task<AddAdressResultDto> AddAdressAsync(AddAdressDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);
        if (user == null)
            return new()
            {
                Success = false,
                Message = "Kullanıcı Bulunamadı"
            };
        ICollection<AppUser> appUsers = new List<AppUser>();
        context.Users.Attach(user);
        appUsers.Add(user);
        Adress adress = new Adress()
        {
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
            AppUsers = appUsers
        };
        await context.Adresses.AddAsync(adress);
        await context.SaveChangesAsync();
        return new()
        {
            Success = true,
            Message = "Adres Bilgisi Başarıyla Eklendi"
        };
    }
}