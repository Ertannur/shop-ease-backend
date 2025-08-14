using ETicaret.Application.CQRS.Results.Adress;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Adresses;

public class AddAdressCommandRequest : IRequest<AddAdressCommandResult>
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string PostCode { get; set; }
    public Guid UserId { get; set; }
}