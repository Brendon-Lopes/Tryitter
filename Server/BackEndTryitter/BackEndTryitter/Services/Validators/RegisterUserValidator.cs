using BackEndTryitter.Contracts.Authentication;
using FluentValidation;

namespace BackEndTryitter.Services.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.FullName).NotEmpty().WithMessage("FullName is required.")
            .Length(4, 50).WithMessage("FullName must be between 4 and 50 characters.");

        RuleFor(u => u.Username).NotEmpty().WithMessage("Username is required")
            .Length(4, 20).WithMessage("Username must be between 4 and 20 characters.");

        RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid.");

        RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required")
            .Length(6, 20).WithMessage("Password must be between 6 and 20 characters.");

        RuleFor(u => u.CurrentModule)
            .InclusiveBetween(1, 4).WithMessage("CurrentModule must be a number between 1 and 4.");
    }
}