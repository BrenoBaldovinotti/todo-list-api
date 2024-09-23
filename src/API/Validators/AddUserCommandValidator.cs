using Application.Commands;
using Domain.Validators;
using FluentValidation;

namespace API.Validators;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(DomainValidationMessages.IsRequired<AddUserCommand>(x  => x.Username))
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(DomainValidationMessages.IsRequired<AddUserCommand>(x => x.Password))
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Role must be a valid enum value (Admin or Operator)");
    }
}
