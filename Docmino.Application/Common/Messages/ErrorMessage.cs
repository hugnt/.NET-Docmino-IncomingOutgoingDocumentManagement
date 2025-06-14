namespace Docmino.Application.Common.Messages;
public class ErrorMessage
{
    public static string ConcurrencyConlict = "Dữ liệu đã bị thay đổi bởi người khác, vui lòng thử lại!";
    public static string UserHasNoPermission = "Bạn không có quyền thực hiện tác vụ này!";
    public static string ObjectNotFound(object value, string objName = "") => $"{objName} '{value}' không tồn tại!";
    public static string ObjectExisted(object value, string objName = "") => $"{objName} '{value}' đã tồn tại!";

    public static string ObjectCanNotBeModified(object value, string objName = "") => $"{objName} '{value}' không được phép cập nhật!";
    public static string ObjectCanNotBeDeleted(object value, string objName = "") => $"{objName} '{value}' không thể bị xóa!";
    public static string ObjectCanNotBeUpdated(object value, string objName = "") => $"{objName} '{value}' không thể được cập nhật!";
    public static string ObjectIsInOtherProcess(object value, string objName = "") => $"{objName} '{value}' không thể xóa do đã trong một tiến trình!";

    public static string ObjectCanNotBeNullOrEmpty(string objName = "") => $"{objName} không được để trống!";
    public static string ServerError() => $"Đã xảy ra lỗi, vui lòng thử lại!";
}