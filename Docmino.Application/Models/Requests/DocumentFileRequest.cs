using Docmino.Domain.Enums;
namespace Docmino.Application.Models.Requests;
public class DocumentFileRequest
{
    public Guid? Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public string? FileUrl { get; set; }
    public double? FileSize { get; set; }
    public FileEncoding? FileEncoding { get; set; }
    public string? Description { get; set; }
}
