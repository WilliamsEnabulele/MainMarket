using FluentValidation;
using MainMarket.AuthAPI.Models.DTO;

namespace MainMarket.AuthAPI.Models.Validation;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .EmailAddress()
            .WithMessage("Invalid email address");
        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number cannot be empty");
    }
}