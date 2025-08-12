using ETicaret.Application.Abstractions;
using ETicaret.Application.Configurations;
using ETicaret.Application.CQRS.Commands.Users;
using ETicaret.Application.CQRS.Results.Users;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Users;

public class UserChangePasswordCommandHandler(IUserService userService) : IRequestHandler<UserChangePasswordCommandRequest, UserChangePasswordCommandResult>
{
    public async Task<UserChangePasswordCommandResult> Handle(UserChangePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var userChangePasswordDto = ModelMapper.MapUserChangePasswordDto(request);
        var result = await userService.ChangePasswordAsync(userChangePasswordDto);
        return new()
        {
            Success = result.Success,
            Message = result.Message,
        };
    }
}