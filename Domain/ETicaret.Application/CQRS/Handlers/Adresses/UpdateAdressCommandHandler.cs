using ETicaret.Application.Abstractions;
using ETicaret.Application.Configurations;
using ETicaret.Application.CQRS.Commands.Adresses;
using ETicaret.Application.CQRS.Results.Adress;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Adresses;

public class UpdateAdressCommandHandler(IAdressService adressService) : IRequestHandler<UpdateAdressCommandRequest, UpdateAdressCommandResult>
{
    public async Task<UpdateAdressCommandResult> Handle(UpdateAdressCommandRequest request, CancellationToken cancellationToken)
    {
        var updateAdressDto = ModelMapper.MapUpdateAdressDto(request);
        var result = await adressService.UpdadeAdressAsync(updateAdressDto);
        return new()
        {
            Success = result.Success,
            Message = result.Message,
        };
    }
}