using ETicaret.Application.CQRS.Commands.Auths;
using FluentValidation;

namespace ETicaret.API.Validations;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommandRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email Boş Geçilemez")
            .EmailAddress().WithMessage("Email Adresini Formata Uygun Yazınız");
        RuleFor(x=>x.NewPassword).NotEmpty().WithMessage("Şifre Bilgisi Boş Geçilemez")
            .MinimumLength(7).WithMessage("Şifreniz Minimum 7 Karakter Olmalıdır.")
            .Matches(@"[A-Z]").WithMessage("Şifreniz En az Bir Büyük Harf İçermeli.")
            .Matches(@"\d").WithMessage("Şifreniz En Az Bir Sayı İçermeli.")
            .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("Şifreniz En Az 1 Özel Karakter İçermelidir");
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token Boş Geçilemez");
    }
}