using ETicaret.Domain.Enums;

namespace ETicaret.Application.DTOs.Users.Results;

public class User
{
    public Guid UserId { get; set; }
    public string Email { get; set; } =  string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly? BirthDate { get; set; } 
    public Gender? Gender { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new List<string>();
}