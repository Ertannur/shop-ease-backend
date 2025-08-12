using ETicaret.Application.Abstractions;
using ETicaret.Application.Configurations;
using ETicaret.Application.CQRS.Commands.Auths;
using ETicaret.Application.CQRS.Results.Auths;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Auths;

public class ResetPasswordCommandHandler(IAuthService authService) : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResult>
{
    public async Task<ResetPasswordCommandResult> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var resetPasswordDto = ModelMapper.MapResetPasswordDto(request);
        var result = await authService.ResetPasswordAsync(resetPasswordDto);
        return new()
        {
            Success = result.Success,
            Message = result.Message
        };
    }
}