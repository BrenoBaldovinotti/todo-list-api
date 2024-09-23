using Application.Commands;
using Domain.Validators;
using FluentValidation;

namespace API.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(DomainValidationMessages.IsRequired<LoginCommand>(x => x.Username));

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(DomainValidationMessages.IsRequired<LoginCommand>(x => x.Password));
    }
}
