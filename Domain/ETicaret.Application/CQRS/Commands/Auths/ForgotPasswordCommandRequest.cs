using ETicaret.Application.CQRS.Results.Auths;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Auths;

public class ForgotPasswordCommandRequest : IRequest<ForgotPasswordCommandResult>
{
    public string Email { get; set; }
}