using FluentValidation;

public class OrganizationValidator : AbstractValidator<OrganizationRequest>
{
    public OrganizationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên tổ chức không được để trống.")
            .MaximumLength(200).WithMessage("Tên tổ chức tối đa 200 ký tự.");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Số điện thoại không được để trống.")
            .MaximumLength(20).WithMessage("Số điện thoại tối đa 20 ký tự.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email không được để trống.")
            .EmailAddress().WithMessage("Email không hợp lệ.");
        RuleFor(x => x.ContactPersonName)
            .NotEmpty().WithMessage("Tên người liên hệ không được để trống.")
            .MaximumLength(100).WithMessage("Tên người liên hệ tối đa 100 ký tự.");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả tối đa 500 ký tự.");
    }
}