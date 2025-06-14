using Docmino.Application.Models.Requests;
using Docmino.Application.Validators;
using FluentValidation;

public class ExternalIncomingDocumentValidator : AbstractValidator<ExternalIncomingDocumentRequest>
{
    public ExternalIncomingDocumentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Bạn cần phải nhập tên.")
            .MaximumLength(200).WithMessage("Tên không được vượt quá 200 ký tự.");

        RuleFor(x => x.ArrivalNumber)
            .NotEmpty().WithMessage("Bạn cần phải nhập số đến.")
            .MaximumLength(50).WithMessage("Số đến không được vượt quá 50 ký tự.")
            .Matches(@"^[\d/-]+$").WithMessage("Số đến chỉ được chứa ký tự số, dấu '-' và '/'.");

        RuleFor(x => x.ArrivalDate)
            .NotEmpty().WithMessage("Bạn cần phải chọn ngày đến.");

        RuleFor(x => x.CodeNotation)
            .NotEmpty().WithMessage("Bạn cần phải nhập ký hiệu.")
            .MaximumLength(100).WithMessage("Ký hiệu không được vượt quá 100 ký tự.")
            .Matches(@"^\d+\/[A-Za-zÀ-ỹà-ỹ\s]+[-–][A-Za-zÀ-ỹà-ỹ\s]+$")
            .WithMessage("Số ký hiệu vb phải có định dạng [Số thứ tự]/[Chữ viết tắt cơ quan ban hành]–[Loại văn bản].");

        RuleFor(x => x.DocumentRegisterId)
            .NotEmpty().WithMessage("Bạn cần phải chọn sổ văn bản.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Bạn cần phải chọn loại văn bản.");

        RuleFor(x => x.FieldId)
            .GreaterThan(0).WithMessage("Bạn cần phải chọn lĩnh vực.");

        RuleFor(x => x.OrganizationId)
            .GreaterThan(0).WithMessage("Bạn cần phải chọn cơ quan.");

        RuleFor(x => x.IssuedDate)
            .NotEmpty().WithMessage("Bạn cần phải chọn ngày ban hành.");

        RuleFor(x => x.PageAmount)
            .GreaterThan(0).WithMessage("Số trang phải lớn hơn 0.");

        RuleFor(x => x.IssuedAmount)
            .GreaterThan(0).WithMessage("Số bản phát hành phải lớn hơn 0.");


        RuleFor(x => x.ConfirmProcess)
            .SetValidator(new ConfirmProcessValidator())
            .When(x => x.ConfirmProcess != null);

    }
}

public class ExternalOutgoingDocumentValidator : AbstractValidator<ExternalOutgoingDocumentRequest>
{
    public ExternalOutgoingDocumentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Bạn cần phải nhập tên.")
            .MaximumLength(200).WithMessage("Tên không được vượt quá 200 ký tự.");

        RuleFor(x => x.CodeNumber)
            .NotEmpty().WithMessage("Bạn cần phải nhập số đi.")
            .MaximumLength(50).WithMessage("Số đi không được vượt quá 50 ký tự.")
            .Matches(@"^[\d/-]+$").WithMessage("Số đi chỉ được chứa ký tự số, dấu '-' và '/'.");

        RuleFor(x => x.CodeNotation)
            .NotEmpty().WithMessage("Bạn cần phải nhập ký hiệu.")
            .MaximumLength(100).WithMessage("Ký hiệu không được vượt quá 100 ký tự.")
            .Matches(@"^\d+\/[A-Za-zÀ-ỹà-ỹ\s]+[-–][A-Za-zÀ-ỹà-ỹ\s]+$")
            .WithMessage("Số ký hiệu vb phải có định dạng [Số thứ tự]/[Chữ viết tắt cơ quan ban hành]–[Loại văn bản].");

        RuleFor(x => x.DocumentRegisterId)
            .NotEmpty().WithMessage("Bạn cần phải chọn sổ văn bản.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Bạn cần phải chọn loại văn bản.");

        RuleFor(x => x.FieldId)
            .GreaterThan(0).WithMessage("Bạn cần phải chọn lĩnh vực.");

        RuleFor(x => x.OrganizationId)
            .GreaterThan(0).WithMessage("Bạn cần phải chọn cơ quan.");

        RuleFor(x => x.IssuedDate)
            .NotEmpty().WithMessage("Bạn cần phải chọn ngày ban hành.");

        RuleFor(x => x.PageAmount)
            .GreaterThan(0).WithMessage("Số trang phải lớn hơn 0.");

        RuleFor(x => x.IssuedAmount)
            .GreaterThan(0).WithMessage("Số bản phát hành phải lớn hơn 0.");


        RuleFor(x => x.ConfirmProcess)
            .SetValidator(new ConfirmProcessValidator())
            .When(x => x.ConfirmProcess != null);

    }
}
