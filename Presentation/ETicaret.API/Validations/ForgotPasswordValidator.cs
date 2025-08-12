using ETicaret.Application.CQRS.Commands.Auths;
using FluentValidation;

namespace ETicaret.API.Validations;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommandRequest>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("E Mail Bilgisi Boş Geçilemez")
            .EmailAddress().WithMessage("E Mail Adresini Formata Uygun Yazınız");
    }
}