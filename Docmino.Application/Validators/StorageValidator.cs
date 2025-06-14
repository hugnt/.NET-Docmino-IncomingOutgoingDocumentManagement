using Docmino.Application.Models.Requests;
using FluentValidation;

namespace Docmino.Application.Validators;

public class StorageValidator : AbstractValidator<StorageRequest>
{
    public StorageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên hồ sơ không được để trống!")
            .MaximumLength(100).WithMessage("Tên hồ sơ không được vượt quá 100 ký tự!");

        RuleFor(x => x.Year)
              .NotNull().WithMessage("Năm không được để trống!")
              .InclusiveBetween(1000, 9999).WithMessage("Năm phải gồm 4 chữ số!");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Mã hồ sơ không được để trống!")
            .Matches(@"^(?i)hs-.+").WithMessage("Mã hồ sơ phải bắt đầu bằng 'HS-' (không phân biệt chữ hoa/thường) và có thêm ký tự phía sau!");

        RuleFor(x => x.Description)
            .MaximumLength(250).WithMessage("Mô tả không được vượt quá 250 ký tự!");
    }
}