namespace ETicaret.Application.DTOs.Users.Results;

public class GetCurrentUserResultDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public List<string> Roles { get; set; } = new List<string>();
}