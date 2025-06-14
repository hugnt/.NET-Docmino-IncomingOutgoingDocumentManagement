namespace Docmino.Application.Common.Messages;

public class SuccessMessage
{
    public static string CreatedSuccessfully(string objName = "") => $"Đã thêm {objName} thành công!";
    public static string UpdatedSuccessfully(string objName = "") => $"Đã cập nhật {objName} thành công!";
    public static string UpdatedSuccessfully(Guid id, string objName = "") => $"Đã cập nhật {objName} với id = {id} thành công!";
    public static string DeletedSuccessfully(Guid id, string objName = "") => $"Đã xóa {objName} với id = {id} thành công!";
    public static string DeletedSuccessfully(string objName = "") => $"Đã xóa {objName} thành công!";
}
