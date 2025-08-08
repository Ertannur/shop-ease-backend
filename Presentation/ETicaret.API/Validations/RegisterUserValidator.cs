using ETicaret.Application.CQRS.Commands.Auths;
using FluentValidation;

namespace ETicaret.API.Validations;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommandRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x=> x.FirstName).NotEmpty().WithMessage("Ad Bilgisi Boş Geçilemez");
        RuleFor(x=>x.LastName).NotEmpty().WithMessage("Soyad Bilgisi Boş Geçilemez");
        RuleFor(x=> x.Email).NotEmpty().WithMessage("Email Bilgisi Boş Geçilemez");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası Boş Geçilemez");
        RuleFor(x=>x.Password).NotEmpty().WithMessage("Şifre Bilgisi Boş Geçilemez");

        RuleFor(x => x.Email).EmailAddress().WithMessage("E Mail Adresinizi Formata Uygun Yazınız");
        RuleFor(x=>x.PhoneNumber).Matches(@"^0?\d{10}$").WithMessage("Telefon Numarasını Doğru Yazınız");
        RuleFor(x => x.DateOfBirth).Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Doğum Tarihiniz Gelecek Bir Tarih Olamaz");
    }
}