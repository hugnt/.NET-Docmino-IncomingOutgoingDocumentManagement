using FluentValidation;

public class DocumentFieldValidator : AbstractValidator<DocumentFieldRequest>
{
    public DocumentFieldValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên lĩnh vực không được để trống.")
            .MaximumLength(200).WithMessage("Tên lĩnh vực tối đa 200 ký tự.");
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Mã lĩnh vực không được để trống.")
            .MaximumLength(50).WithMessage("Mã lĩnh vực tối đa 50 ký tự.");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả tối đa 500 ký tự.");
    }
}