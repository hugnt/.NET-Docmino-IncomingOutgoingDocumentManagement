using Docmino.Application.Models;

public interface IDocumentFieldService
{
    Task<Result> GetAll(FilterRequest filter);
    Task<Result> GetById(int id);
    Task<Result> Add(DocumentFieldRequest modelRequest);
    Task<Result> Update(int id, DocumentFieldRequest modelRequest);
    Task<Result> Delete(int id);
}