using Application.DTOs;
using Domain.Validators;
using FluentValidation;

namespace API.Validators;

public class UpdateIssueRequestDtoValidator : AbstractValidator<UpdateIssueRequestDto>
{
    public UpdateIssueRequestDtoValidator()
    {
        RuleFor(item => item.Name)
           .NotEmpty().WithMessage(DomainValidationMessages.IsRequired<UpdateIssueRequestDto>(o => o.Name));
    }
}
