using Docmino.Application.Models;
using Docmino.Application.Models.Requests;

namespace Docmino.Application.Services;
public interface IDocumentCategoryService
{
    public Task<Result> GetAll(FilterRequest filter);
    public Task<Result> GetById(int id);
    public Task<Result> Add(DocumentCategoryRequest modelRequest);
    public Task<Result> Update(int id, DocumentCategoryRequest modelRequest);
    public Task<Result> Delete(int id);
}
