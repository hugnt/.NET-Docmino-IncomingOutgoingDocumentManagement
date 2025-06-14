using Docmino.Application.Models;
using Docmino.Application.Models.Requests;

namespace Docmino.Application.Services;
public interface IStoragePeriodService
{
    Task<Result> Lookup();
    Task<Result> GetAll(FilterRequest filter);
    Task<Result> GetById(int id);
    Task<Result> Add(StoragePeriodRequest modelRequest);
    Task<Result> Update(int id, StoragePeriodRequest modelRequest);
    Task<Result> Delete(int id);
}
