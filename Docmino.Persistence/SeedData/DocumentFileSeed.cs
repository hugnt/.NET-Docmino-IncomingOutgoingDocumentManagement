using Docmino.Domain.Entities;

namespace Docmino.Persistence.SeedData;

public class DocumentFileSeed
{
    private static readonly DateTime _defaultTime = new(2025, 01, 01);
    public static IEnumerable<DocumentFile> DocumentFiles => new List<DocumentFile>()
    {
        
    };
}
