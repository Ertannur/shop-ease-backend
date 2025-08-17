using ETicaret.Application.CQRS.Results.Auths;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Auths;

public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResult>
{
    public string RefreshToken { get; set; }
}