using FluentValidation;
using MainMarket.AuthAPI.Models.DTO;

namespace MainMarket.AuthAPI.Models.Validation;

public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty");
        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty");
    }
}
