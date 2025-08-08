using ETicaret.Domain.Enums;

namespace ETicaret.Application.DTOs.Auths.Requests;

public class RegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
}