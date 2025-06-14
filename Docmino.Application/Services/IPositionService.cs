using Docmino.Application.Models;
using Docmino.Application.Models.Requests;
namespace Docmino.Application.Services;

public interface IPositionService
{
    Task<Result> Lookup();
    Task<Result> GetAll(FilterRequest filter);
    Task<Result> GetById(int id);
    Task<Result> Add(PositionRequest modelRequest);
    Task<Result> Update(int id, PositionRequest modelRequest);
    Task<Result> Delete(int id);
}