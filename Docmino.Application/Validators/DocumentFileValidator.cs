using Docmino.Application.Models.Requests;
using FluentValidation;

namespace Docmino.Application.Validators;
public class DocumentFileValidator : AbstractValidator<DocumentFileRequest>
{
    public DocumentFileValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("FileName is required.")
            .MaximumLength(255).WithMessage("FileName must not exceed 255 characters.");

        RuleFor(x => x.FileType)
            .NotEmpty().WithMessage("FileType is required.")
            .MaximumLength(50).WithMessage("FileType must not exceed 50 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
