using ETicaret.Application.CQRS.Queries.Products;
using FluentValidation;

namespace ETicaret.API.Validations;

public class GetProductsValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsValidator()
    {
        RuleFor(x => x.CurrentPage).GreaterThanOrEqualTo(0).WithMessage("Geçerli Sayfa Bilgisi Negatif Olamaz");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(0).WithMessage("Sayfa Sayısı Negatif Olamaz");
    }
}