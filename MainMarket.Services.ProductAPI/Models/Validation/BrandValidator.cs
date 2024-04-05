using FluentValidation;
using MainMarket.Services.ProductAPI.Models.DTO;

namespace MainMarket.Services.ProductAPI.Models.Validation;

public class BrandValidator : AbstractValidator<BrandRequest>
{
    public BrandValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty()
            .WithMessage("Brand name cannot be empty");
    }
}
