using BackEndTryitter.Contracts.User;
using FluentValidation;

namespace BackEndTryitter.Services.Validators;

public class UpdateUserStatusValidator : AbstractValidator<UpdateUserStatusRequest>
{
    public UpdateUserStatusValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .MaximumLength(100).WithMessage("Status must not exceed 100 characters");
    }
}