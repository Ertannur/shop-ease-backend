using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Adresses;
using ETicaret.Application.CQRS.Results.Adress;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Adresses;

public class DeleteAdressCommandHandler(IAdressService adressService) : IRequestHandler<DeleteAdressCommandRequest,  DeleteAdressCommandResult>
{
    public async Task<DeleteAdressCommandResult> Handle(DeleteAdressCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await adressService.DeleteAdressAsync(request.AdressId);
        return new()
        {
            Success = result.Success,
            Message = result.Message
        };
    }
}