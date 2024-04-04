using FluentValidation;
using MainMarket.AuthAPI.Models.DTO;

namespace MainMarket.AuthAPI.Models.Validation;

public class AssignRoleRequestValidator : AbstractValidator<AssignRoleRequest>
{
    public AssignRoleRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .EmailAddress()
            .WithMessage("Enter valid email");
        RuleFor(r => r.Role)
            .NotEmpty()
            .WithMessage("Role cannot be empty");
    }
}
