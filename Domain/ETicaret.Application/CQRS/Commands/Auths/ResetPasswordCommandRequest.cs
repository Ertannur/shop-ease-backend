using ETicaret.Application.CQRS.Results.Auths;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Auths;

public class ResetPasswordCommandRequest : IRequest<ResetPasswordCommandResult>
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string Token { get; set; }
}