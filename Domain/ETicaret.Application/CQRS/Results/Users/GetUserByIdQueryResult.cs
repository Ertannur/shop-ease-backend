using ETicaret.Domain.Enums;

namespace ETicaret.Application.CQRS.Results.Users;

public class GetUserByIdQueryResult
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public Gender Gender { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}