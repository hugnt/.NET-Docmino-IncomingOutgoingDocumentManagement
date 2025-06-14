using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Responses;
public class DocumentFileResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public double FileSize { get; set; }
    public FileEncoding FileEncoding { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
