using ETicaret.Application.CQRS.Queries.Adresses;
using ETicaret.Application.DTOs.Adresses.Requests;
using ETicaret.Application.DTOs.Adresses.Results;

namespace ETicaret.Application.Abstractions;

public interface IAdressService
{
    Task<AddAdressResultDto> AddAdressAsync(AddAdressDto dto);
    Task<IEnumerable<GetUserAdressResultDto>> GetUserAdressAsync(Guid userId);
}