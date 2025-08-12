using ETicaret.Application.CQRS.Results.Users;
using ETicaret.Domain.Enums;
using MediatR;

namespace ETicaret.Application.CQRS.Commands.Users;

public class UpdateUserCommandRequest : IRequest<UpdateUserCommandResult>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public Gender Gender { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}