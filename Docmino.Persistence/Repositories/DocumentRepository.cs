using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using Docmino.Persistence.Repositories.Base;

namespace Docmino.Persistence.Repositories;
public class DocumentRepository : Repository<Document>, IDocumentRepository
{
    public DocumentRepository(AppDbContext context) : base(context)
    {

    }
}
