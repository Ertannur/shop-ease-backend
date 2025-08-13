using ETicaret.Application.Abstractions;
using ETicaret.Application.Configurations;
using ETicaret.Application.CQRS.Commands.Adresses;
using ETicaret.Application.CQRS.Results.Adress;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Adresses;

public class AddAdressCommandHandler(IAdressService adressService) : IRequestHandler<AddAdressCommandRequest, AddAdressCommandResult>
{
    public async Task<AddAdressCommandResult> Handle(AddAdressCommandRequest request, CancellationToken cancellationToken)
    {
        var adressDto = ModelMapper.MapAddAdressDto(request);
        var result = await  adressService.AddAdressAsync(adressDto);
        return new AddAdressCommandResult()
        {
            Success = result.Success,
            Message = result.Message,
        };
    }
}