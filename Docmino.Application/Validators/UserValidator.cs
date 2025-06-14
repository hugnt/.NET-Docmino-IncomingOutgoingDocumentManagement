using Docmino.Application.Models.Requests;
using FluentValidation;

namespace Docmino.Application.Validators;
public sealed class AddUserValidator : AbstractValidator<AddUserRequest>
{
    public AddUserValidator()
    {
        RuleFor(x => x.Fullname)
            .NotEmpty().WithMessage("Fullname must not be empty!")
            .MaximumLength(50).WithMessage("Fullname must not exceed 50 characters!");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must not be empty!")
            .EmailAddress().WithMessage("Invalid email format!");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username must not be empty!")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters!")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters!");

        RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password must not be empty!")
                    .MinimumLength(8).WithMessage("Password must be at least 8 characters!");

    }
}

public sealed class UserValidator : AbstractValidator<UserRequest>
{
    public UserValidator()
    {
        RuleFor(x => x.Fullname)
            .NotEmpty().WithMessage("Fullname must not be empty!")
            .MaximumLength(50).WithMessage("Fullname must not exceed 50 characters!");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must not be empty!")
            .EmailAddress().WithMessage("Invalid email format!");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username must not be empty!")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters!")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters!");
    }
}
