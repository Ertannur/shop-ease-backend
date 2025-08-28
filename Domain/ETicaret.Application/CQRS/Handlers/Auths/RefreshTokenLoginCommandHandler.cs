using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Auths;
using ETicaret.Application.CQRS.Results.Auths;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Auths;

public class RefreshTokenLoginCommandHandler(IAuthService authService) : IRequestHandler<RefreshTokenLoginCommandRequest,RefreshTokenLoginCommandResult>
{
    public async Task<RefreshTokenLoginCommandResult> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.RefreshTokenLoginAsync(request.RefreshToken);
        return new RefreshTokenLoginCommandResult()
        {
            Success = result.Success,
            Message = result.Message,
            Token = result.Token,
            User = result.User
        };
    }
}