using ETicaret.Application.CQRS.Commands.Adresses;
using FluentValidation;

namespace ETicaret.API.Validations;

public class AddAdressValidator  : AbstractValidator<AddAdressCommandRequest>
{
    public AddAdressValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Ad Bilgisi Zorunludur")
            .MinimumLength(2).WithMessage("Minimum 2 Karakter Olmalıdır");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyadı Bilgisi Zorunludur")
            .MinimumLength(2).WithMessage("Minimum 2 Karakter Olmalıdır");
        RuleFor(x => x.City).NotEmpty().WithMessage("Şehir Bilgisi Zorunludur")
            .MinimumLength(2).WithMessage("Minimum 2 Karakter Olmalıdır");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Adres Bilgisi Zorunludur")
            .MinimumLength(2).WithMessage("Minimum 2 Karakter Olmalıdır");
        RuleFor(x => x.District).NotEmpty().WithMessage("İlçe Bilgisi Zorunludur")
            .MinimumLength(2).WithMessage("Minimum 2 Karakter Olmalıdır");
        RuleFor(x => x.PostCode).NotEmpty().WithMessage("Posta Kodu Bilgisi Zorunludur")
            .MinimumLength(2).WithMessage("Minimum 2 Karakter Olmalıdır");
        RuleFor(x=> x.Email).NotEmpty().WithMessage("Email Adresi Zorunludur")
            .EmailAddress().WithMessage("Email Adresi Zorunludur");
        RuleFor(x=>x.Phone).Matches(@"^0?\d{10}$").WithMessage("Telefon Numarasını Doğru Yazınız");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Adres Başlığı Zorunludur")
            .MinimumLength(2).WithMessage("Minimum 2 Karakter Olmalıdır");

    }
}