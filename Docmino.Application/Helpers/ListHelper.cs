namespace Docmino.Application.Helpers;
public static class ListHelper
{
    public static bool IsEmpty<T>(this List<T>? list)
    {
        return list == null || list.Count == 0;
    }

    public static bool IsEmpty<T>(this IEnumerable<T>? list)
    {
        return list == null || list.Count() == 0;
    }
}
