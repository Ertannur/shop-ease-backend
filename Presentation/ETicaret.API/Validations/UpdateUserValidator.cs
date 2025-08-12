using ETicaret.Application.CQRS.Commands.Users;
using FluentValidation;

namespace ETicaret.API.Validations;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommandRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Bilgisi Boş Geçilemez");
        RuleFor(x => x.Email).NotEmpty()
            .WithMessage("Email Bilgisi Boş Geçilemez")
            .EmailAddress()
            .WithMessage("E Posta Adresini Formata Uygun Yazınız");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Bilgisi Boş Geçilemez.")
            .MinimumLength(7).WithMessage("Şifreniz Minimum 7 Karakter Olmalıdır.")
            .Matches(@"[A-Z]").WithMessage("Şifreniz En az Bir Büyük Harf İçermeli.")
            .Matches(@"\d").WithMessage("Şifreniz En Az Bir Sayı İçermeli.")
            .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("Şifreniz En Az 1 Özel Karakter İçermelidir");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad Bilgisi Boş Geçilemez.")
            .MinimumLength(2).WithMessage("İsim Bilgisi Minimum 2 Karakter Olmalıdır");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyadı Bilgisi Boş Geçilemez.")
            .MinimumLength(2).WithMessage("Soyadı Bilgisi Minimum 2 Karakter Olmalıdır");
        RuleFor(x=>x.PhoneNumber).Matches(@"^0?\d{10}$").WithMessage("Telefon Numarasını Doğru Yazınız")
            .NotEmpty().WithMessage("Telefon Numarası Bilgisi Boş Geçilemez");
        RuleFor(x => x.DateOfBirth).Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Doğum Tarihiniz Bugünden Büyük Olamaz")
            .NotEmpty().WithMessage("Doğum Tarihi Bilgisi Boş Geçilemez.");
    }
}