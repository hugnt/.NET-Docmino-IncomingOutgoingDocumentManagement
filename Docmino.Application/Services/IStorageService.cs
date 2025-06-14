using Docmino.Application.Models;
using Docmino.Application.Models.Requests;

namespace Docmino.Application.Services;
public interface IStorageService
{
    Task<Result> GetAll(StorageFilterRequest filter);
    Task<Result> GetDetail(Guid id);
    Task<Result> Add(StorageRequest modelRequest);
    Task<Result> Update(Guid id, StorageRequest modelRequest);
    Task<Result> UpdateDocuments(Guid id, StorageDocumentsRequest modelRequest);
    Task<Result> Delete(Guid id);
}
