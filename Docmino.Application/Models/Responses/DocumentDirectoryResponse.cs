using Docmino.Domain.Enums;

namespace Docmino.Application.Models.Responses;
public class DirectoryTreeItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentDirectoryId { get; set; }
    public DirectoryType Type { get; set; }
    public int DocumentCount { get; set; }
}

public class DocumentDirectoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? ParentDirectoryId { get; set; }
    public string? ParentDirectoryName { get; set; }
    public DirectoryType Type { get; set; }
    public DateTime CreatedAt { get; set; }

}