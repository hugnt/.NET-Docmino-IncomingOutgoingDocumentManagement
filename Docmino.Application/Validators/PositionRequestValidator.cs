using Docmino.Application.Models.Requests;
using FluentValidation;

namespace Docmino.Application.Validators
{
    public class PositionRequestValidator : AbstractValidator<PositionRequest>
    {
        public PositionRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be at most 100 characters.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("DepartmentId must be greater than 0.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description must be at most 250 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}