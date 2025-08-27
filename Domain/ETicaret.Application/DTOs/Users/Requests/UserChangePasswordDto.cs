namespace ETicaret.Application.DTOs.Users.Requests;

public class UserChangePasswordDto
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}