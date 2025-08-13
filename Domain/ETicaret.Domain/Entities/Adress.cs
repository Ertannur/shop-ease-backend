using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities;

public class Adress : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string PostCode { get; set; }
    public ICollection<AppUser> AppUsers { get; set; }
}
