using ETicaret.Application.CQRS.Results.Adress;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Adresses;

public class DeleteAdressCommandRequest : IRequest<DeleteAdressCommandResult>
{
    public Guid AdressId { get; set; }
}