using Docmino.Application.Models.Requests;
using FluentValidation;

namespace Docmino.Application.Validators;

public class DocumentDirectoryValidator : AbstractValidator<DocumentDirectoryRequest>
{
    public DocumentDirectoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name of Directory must not be empty!")
            .MaximumLength(100).WithMessage("Name of Directory must not exceed 100 characters!");

        RuleFor(x => x.Description)
            .MaximumLength(250).WithMessage("Description must not exceed 250 characters!");

    }
}
