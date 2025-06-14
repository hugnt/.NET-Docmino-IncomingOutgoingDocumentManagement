using Docmino.Application.Models;
using Docmino.Domain.Enums;

namespace Docmino.Application.Helpers;
public static class EnumHelper
{
    public static string GetDocumentTypeName(this DocumentType documentStatus) => documentStatus switch
    {
        DocumentType.Incomming => "Văn bản đến",
        DocumentType.Outgoing => "Văn bản đi",
        DocumentType.InternalOutgoing => "Văn bản nội bộ đi",
        DocumentType.InternalIncomming => "Văn bản nội bộ đến",
        _ => "Không xác định"
    };

    public static string GetDocumentStatusName(this DocumentStatus documentStatus) => documentStatus switch
    {
        DocumentStatus.Draff => "Bản nháp",
        DocumentStatus.InProcess => "Đang xử lý",
        DocumentStatus.Published => "Đã xuất bản",
        DocumentStatus.Cancel => "Đã hủy",
        _ => "Không xác định"
    };

    public static string GetSecurePriorityName(this SecurePriority securePriority) => securePriority switch
    {
        SecurePriority.Normal => "Bình thường",
        SecurePriority.Low => "Thấp",
        SecurePriority.High => "Cao",
        SecurePriority.Secure => "Bảo mật",
        SecurePriority.SuperSecure => "Siêu bảo mật",
        _ => "Không xác định"
    };

    public static string GetUrgentPriorityName(this UrgentPriority urgentPriority) => urgentPriority switch
    {
        UrgentPriority.Normal => "Bình thường",
        UrgentPriority.Low => "Thấp",
        UrgentPriority.High => "Cao",
        UrgentPriority.Urgent => "Khẩn cấp",
        UrgentPriority.SuperUrgent => "Siêu khẩn cấp",
        _ => "Không xác định"
    };

    public static string GetProcessTypeName(this ProcessType processType) => processType switch
    {
        ProcessType.None => "Không xác định",
        ProcessType.Secure => "Bảo mật",
        ProcessType.Normal => "Bình thường",
        ProcessType.Important => "Quan trọng",
        _ => "Không xác định"
    };

    public static string GetReviewerTypeName(this ReviewerType reviewerType) => reviewerType switch
    {
        ReviewerType.User => "Người dùng",
        ReviewerType.Group => "Nhóm",
        ReviewerType.Position => "Chức vụ",
        ReviewerType.Department => "Phòng ban",
        _ => "Không xác định"
    };

    public static string GetSignTypeName(this SignType signType) => signType switch
    {
        SignType.None => "Chỉ duyệt",
        SignType.Image => "Chữ ký hình ảnh",
        SignType.DigitalSignature => "Chữ ký số",
        SignType.Blockchain => "Blockchain",
        _ => "Không xác định"
    };


    public static List<Lookup<int>> ToLookupList<TEnum>(Func<TEnum, string> nameSelector) where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
                   .Cast<TEnum>()
                   .Select(e => new Lookup<int>
                   {
                       Id = Convert.ToInt32(e),
                       Name = nameSelector(e)
                   })
                   .ToList();
    }
}
