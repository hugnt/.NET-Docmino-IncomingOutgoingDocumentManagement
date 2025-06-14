namespace Docmino.Application.Common.Messages;
public static class UserMessage
{
    public static string UserDontHavePermission(string reason = "") => $"Người dùng không có quyền thực hiện thao tác {reason}";

    public static string UserDontHavePermissionInThisStep(string reason = "") => $"Người dùng không có quyền thực hiện thao tác {reason} ở bước hiện tại";
}

