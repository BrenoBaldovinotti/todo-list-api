using Application.DTOs;
using Domain.Validators;
using FluentValidation;

namespace API.Validators;

public class CreateIssueRequestDtoValidator : AbstractValidator<CreateIssueRequestDto>
{
    public CreateIssueRequestDtoValidator()
    {
        RuleFor(item => item.Name)
           .NotEmpty().WithMessage(DomainValidationMessages.IsRequired<CreateIssueRequestDto>(o => o.Name));
    }
}
