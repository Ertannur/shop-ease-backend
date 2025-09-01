using ETicaret.Application.CQRS.Commands.Adresses;
using ETicaret.Application.CQRS.Queries.Adresses;
using ETicaret.Application.DTOs.Adresses.Requests;
using ETicaret.Application.DTOs.Adresses.Results;

namespace ETicaret.Application.Abstractions;

public interface IAdressService
{
    Task<AddAdressResultDto> AddAdressAsync(AddAdressDto dto);
    Task<IEnumerable<GetUserAdressResultDto>> GetUserAdressAsync();
    Task<UpdateAdressResultDto>UpdadeAdressAsync(UpdateAdressDto updateAdressDto);
    Task<DeleteAdressResultDto> DeleteAdressAsync(Guid adressId);
}