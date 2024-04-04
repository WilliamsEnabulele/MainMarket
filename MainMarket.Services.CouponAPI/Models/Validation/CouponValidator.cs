using FluentValidation;
using MainMarket.Services.ProductAPI.Models.DTO;

namespace MainMarket.Services.ProductAPI.Models.Validation
{
    public class CouponValidator : AbstractValidator<CouponDto>
    {
        public CouponValidator()
        {
            RuleFor(c => c.CouponCode).NotEmpty().WithMessage("Coupon Code cannot be empty");
            RuleFor(c => c.DiscountAmount).NotEmpty().WithMessage("Discount Amount cannot be empty");
            RuleFor(c => c.MinAmount).NotEmpty().WithMessage("Mininum amount cannot be empty");
        }
    }
}