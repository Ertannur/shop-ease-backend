using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Auths;
using ETicaret.Application.CQRS.Results.Auths;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Auths;

public class ForgotPasswordCommandHandler(IAuthService authService) :  IRequestHandler<ForgotPasswordCommandRequest, ForgotPasswordCommandResult>
{
    public async Task<ForgotPasswordCommandResult> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.ForgotPasswordAsync(request.Email);
        return new()
        {
            Success = result.Success,
            Message = result.Message,
        };
    }
}