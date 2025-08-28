namespace ETicaret.Application.DTOs.Adresses.Requests;

public class UpdateAdressDto
{
    public Guid AdressId { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string PostCode { get; set; }
}