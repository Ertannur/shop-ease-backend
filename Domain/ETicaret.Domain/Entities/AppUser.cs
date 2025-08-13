using ETicaret.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public ICollection<Favorite> Favorites { get; set; }
    public ICollection<Adress> Adresses { get; set; }
}