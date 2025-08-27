using ETicaret.Application.CQRS.Results.Users;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Users;

public class UserChangePasswordCommandRequest : IRequest<UserChangePasswordCommandResult>
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}