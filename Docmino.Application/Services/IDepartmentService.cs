using Docmino.Application.Models;
using Docmino.Application.Models.Requests;

namespace Docmino.Application.Services;
public interface IDepartmentService
{
    public Task<Result> Lookup();
    public Task<Result> GetAll(FilterRequest filter);
    public Task<Result> GetById(int id);
    public Task<Result> Add(DepartmentRequest modelRequest);
    public Task<Result> Update(int id, DepartmentRequest modelRequest);
    public Task<Result> Delete(int id);
}
