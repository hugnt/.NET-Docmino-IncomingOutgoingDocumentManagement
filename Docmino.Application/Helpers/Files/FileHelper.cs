namespace Docmino.Application.Helpers.Files;
public class FileHelper
{
    public static string GetFileExtension(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return string.Empty;
        int lastDotIndex = fileName.LastIndexOf('.');
        if (lastDotIndex < 0 || lastDotIndex == fileName.Length - 1)
            return string.Empty;
        return fileName.Substring(lastDotIndex + 1);
    }

    public static string FormatFilename(string fileName)
    {
        return fileName
            .Replace(" ", "_")
            .Replace(":", "_")
            .Replace("/", "_")
            .Replace("\\", "_")
            .Replace("?", "_")
            .Replace("*", "_")
            .Replace("<", "_")
            .Replace(">", "_")
            .Replace("|", "_")
            .Replace("\"", "_")
            .Replace("(", "_")
            .Replace(")", "_");
    }


}
