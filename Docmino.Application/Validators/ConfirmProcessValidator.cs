using Docmino.Application.Models.Requests;
using Docmino.Domain.Enums;
using FluentValidation;

namespace Docmino.Application.Validators;
public class ConfirmProcessValidator : AbstractValidator<ConfirmProcessRequest>
{
    public ConfirmProcessValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Tên không được vượt quá 100 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Loại quy trình không hợp lệ.");

        RuleFor(x => x.ManagerId)
            .NotEmpty().WithMessage("Mã người quản lý là bắt buộc.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.ProcessDetails)
            .NotNull().WithMessage("Chi tiết quy trình là bắt buộc.")
            .Must(details => details != null && details.Count > 0)
            .WithMessage("Phải có ít nhất một chi tiết quy trình.");

        RuleForEach(x => x.ProcessDetails)
            .SetValidator(new ProcessDetailValidator());
    }
}

public class ProcessDetailValidator : AbstractValidator<ProcessDetailRequest>
{
    public ProcessDetailValidator()
    {
        RuleFor(x => x.StepNumber)
            .GreaterThan(0).WithMessage("Số bước phải lớn hơn 0.");
        RuleFor(x => x.ReviewerType)
            .IsInEnum().WithMessage("Loại người kiểm duyệt không hợp lệ.");

        RuleFor(x => x.ReviewerUserId)
            .NotEmpty().When(x => x.ReviewerType == ReviewerType.User)
            .WithMessage("Người kiểm duyệt là bắt buộc khi loại người kiểm duyệt là Người dùng.");

        RuleFor(x => x.ReviewerGroupId)
            .NotEmpty().When(x => x.ReviewerType == ReviewerType.Group)
            .WithMessage("Nhóm kiểm duyệt là bắt buộc khi loại người kiểm duyệt là Nhóm.");

        RuleFor(x => x.ReviewerPositionId)
            .GreaterThan(0).When(x => x.ReviewerPositionId.HasValue)
            .WithMessage("Vị trí kiểm duyệt phải lớn hơn 0.");

        RuleFor(x => x.ReviewerDepartmentId)
            .GreaterThan(0).When(x => x.ReviewerDepartmentId.HasValue)
            .WithMessage("Phòng ban kiểm duyệt phải lớn hơn 0.");

        RuleFor(x => x.DateStart)
            .LessThanOrEqualTo(x => x.DateEnd).WithMessage("Ngày bắt đầu phải trước hoặc bằng ngày kết thúc.");

        RuleFor(x => x.SignType)
            .IsInEnum().WithMessage("Loại chữ ký không hợp lệ.");

        RuleFor(x => x.SignDetails)
            .NotEmpty()
            .When(x => x.SignType == SignType.Image || x.SignType == SignType.DigitalSignature)
            .WithMessage("Phải có ít nhất một chi tiết chữ ký khi loại chữ ký là Hình ảnh hoặc Chữ ký số.");

        RuleForEach(x => x.SignDetails)
            .SetValidator(new ProcessSignDetailValidator())
            .When(x => x.SignDetails != null);
    }
}

public class ProcessSignDetailValidator : AbstractValidator<ProcessSignDetailRequest>
{
    public ProcessSignDetailValidator()
    {
        RuleFor(x => x.FileIndex)
            .GreaterThanOrEqualTo(0).When(x => x.FileIndex != 0)
            .WithMessage("Chỉ số tệp phải lớn hơn hoặc bằng 0.");

        RuleFor(x => x.PosX)
            .GreaterThanOrEqualTo(0).When(x => x.PosX != 0)
            .WithMessage("Vị trí X phải lớn hơn hoặc bằng 0.");

        RuleFor(x => x.PosY)
            .GreaterThanOrEqualTo(0).When(x => x.PosY != 0)
            .WithMessage("Vị trí Y phải lớn hơn hoặc bằng 0.");

        RuleFor(x => x.SignZoneWidth)
            .GreaterThan(0).When(x => x.SignZoneWidth != 0)
            .WithMessage("Chiều rộng vùng ký phải lớn hơn 0.");

        RuleFor(x => x.SignZoneHeight)
            .GreaterThan(0).When(x => x.SignZoneHeight != 0)
            .WithMessage("Chiều cao vùng ký phải lớn hơn 0.");

        RuleFor(x => x.SignPage)
            .GreaterThanOrEqualTo(1).When(x => x.SignPage.HasValue)
            .WithMessage("Trang ký phải lớn hơn hoặc bằng 1.");

        RuleFor(x => x.TranslateX)
            .NotNull().When(x => x.TranslateX.HasValue)
            .WithMessage("TranslateX không được để trống.");

        RuleFor(x => x.TranslateY)
            .NotNull().When(x => x.TranslateY.HasValue)
            .WithMessage("TranslateY không được để trống.");
    }
}