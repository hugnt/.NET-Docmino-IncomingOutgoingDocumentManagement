using FluentValidation;

public class DocumentRegisterValidator : AbstractValidator<DocumentRegisterRequest>
{
    public DocumentRegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên sổ đăng ký không được để trống.")
            .MaximumLength(200).WithMessage("Tên sổ đăng ký tối đa 200 ký tự.");
        RuleFor(x => x.Year)
            .GreaterThan(2000).WithMessage("Năm phải lớn hơn 2000.");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả tối đa 500 ký tự.");
    }
}