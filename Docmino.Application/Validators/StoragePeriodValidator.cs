using Docmino.Application.Models.Requests;
using FluentValidation;

namespace Docmino.Application.Validators;

public class StoragePeriodValidator : AbstractValidator<StoragePeriodRequest>
{
    public StoragePeriodValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên thời hạn bảo quản không được để trống.")
            .MaximumLength(200).WithMessage("Tên thời hạn bảo quản tối đa 200 ký tự.");
        RuleFor(x => x.YearAmount)
            .GreaterThan(0).WithMessage("Số năm phải lớn hơn 0.");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả tối đa 500 ký tự.");
    }
}
