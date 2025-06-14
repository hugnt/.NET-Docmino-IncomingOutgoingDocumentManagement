using FluentValidation;

public class GroupValidator : AbstractValidator<GroupRequest>
{
    public GroupValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên nhóm không được để trống.")
            .MaximumLength(100).WithMessage("Tên nhóm tối đa 100 ký tự.");
    }
}