using ETicaret.Application.CQRS.Results;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Auths;

public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResult>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}