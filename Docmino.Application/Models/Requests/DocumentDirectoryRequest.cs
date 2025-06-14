using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Requests;

public class DocumentDirectoryFilterRequest : FilterRequest
{
    public DirectoryType? Type { get; set; }
    public Guid? ParentDirectoryId { get; set; }
}

public class DocumentDirectoryRequest
{
    public string Name { get; set; }
    public DirectoryType Type { get; set; }
    public string? Description { get; set; }
    public Guid? ParentDirectoryId { get; set; }
}
