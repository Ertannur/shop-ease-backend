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
            UserId = dto.UserId,
        };
        await context.Adresses.AddAsync(adress);
        await context.SaveChangesAsync();
        return new()
        {
            Success = true,
            Message = "Adres Bilgisi Başarıyla Eklendi"
        };
    }

    public async Task<IEnumerable<GetUserAdressResultDto>> GetUserAdressAsync(Guid userId)
    {
        IEnumerable<Adress> adresses = await context.Adresses.Where(x => x.UserId == userId && x.IsDeleted == false).ToListAsync();
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