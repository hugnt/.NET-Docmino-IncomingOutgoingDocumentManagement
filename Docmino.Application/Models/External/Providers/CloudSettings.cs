namespace Docmino.Application.Models.External.Providers;
public class CloudSettings : CloudinaryStorageOptions
{
    public string UrlFormat { get; set; } = default!;
}

public class CloudinaryStorageOptions
{
    public string CloudName { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
    public string ApiSecret { get; set; } = default!;
    public int ExpireSignatureMinutes { get; set; }
    public MediaSettings MediaSettings { get; set; } = default!;
}

public class MediaSettings
{
    public FileSettings Certificates { get; set; } = default!;
    public FileSettings SignedDocuments { get; set; } = default!;
    public FileSettings Images { get; set; } = default!;
    public FileSettings Videos { get; set; } = default!;
    public FileSettings Documents { get; set; } = default!;
}

public class FileSettings
{
    public string[] AllowedExtensions { get; set; } = default!;
    public long MaxSizeBytes { get; set; } = default!;
    public string FolderPath { get; set; } = default!;
}
