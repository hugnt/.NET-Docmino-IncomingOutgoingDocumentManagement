using Docmino.Application.Models.Requests;
using FluentValidation;

namespace Docmino.Application.Validators;

public class DepartmentValidator : AbstractValidator<DepartmentRequest>
{
    public DepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name of Department must not be empty!")
            .MaximumLength(100).WithMessage("Name of Department must not exceed 100 characters!");

        RuleFor(x => x.Description)
            .MaximumLength(250).WithMessage("Description must not exceed 250 characters!");

    }
}
