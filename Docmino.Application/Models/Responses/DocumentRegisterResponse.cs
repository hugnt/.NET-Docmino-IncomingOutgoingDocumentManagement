using Docmino.Domain.Enums;

public class DocumentRegisterResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
    public DocumentType RegisterType { get; set; }
}