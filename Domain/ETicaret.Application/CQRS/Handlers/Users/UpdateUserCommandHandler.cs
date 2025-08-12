using ETicaret.Application.Abstractions;
using ETicaret.Application.Configurations;
using ETicaret.Application.CQRS.Commands.Users;
using ETicaret.Application.CQRS.Results.Users;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Users;

public class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResult>
{
    public async Task<UpdateUserCommandResult> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var updateUserDto = ModelMapper.MapUpdateUserDto(request);
        var result = await userService.UpdateUserAsync(updateUserDto);
        return new()
        {
            Success = result.Success,
            Message = result.Message,
        };
    }
}