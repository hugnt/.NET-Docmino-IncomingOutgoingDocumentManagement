using Docmino.Application.Models;

public interface IDocumentRegisterService
{
    Task<Result> GetAll(FilterRequest filter);
    Task<Result> GetById(Guid id);
    Task<Result> Add(DocumentRegisterRequest modelRequest);
    Task<Result> Update(Guid id, DocumentRegisterRequest modelRequest);
    Task<Result> Delete(Guid id);
}