using Docmino.Application.Models;

namespace Docmino.Application.Services;
public interface IDocumentFileService
{
    public Task<Result> GetFileUrl(Guid documentFileId);
}
