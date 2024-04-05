using FluentValidation;
using MainMarket.Services.ProductAPI.Models.DTO;

namespace MainMarket.Services.ProductAPI.Models.Validation
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty()
                .WithMessage("Category Id cannot be empty");
            RuleFor(c => c.BrandId)
                .NotEmpty()
                .WithMessage("Brand Id cannot be empty");
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Product name cannot be empty");
            RuleFor(c => c.Price)
                .NotEmpty()
                .WithMessage("Product Price cannot be empty");
        }
    }
}