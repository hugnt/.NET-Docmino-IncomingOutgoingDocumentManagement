using Docmino.Application.Models;
using Docmino.Application.Models.Requests;

namespace Docmino.Application.Services;
public interface IDocumentDirectoryService
{
    public Task<Result> GetDirectoryTree();
    public Task<Result> GetAll(DocumentDirectoryFilterRequest filter);
    public Task<Result> GetDetail(Guid id);
    public Task<Result> Add(DocumentDirectoryRequest modelRequest);
    public Task<Result> Update(Guid id, DocumentDirectoryRequest modelRequest);
    public Task<Result> Delete(Guid id);
}
