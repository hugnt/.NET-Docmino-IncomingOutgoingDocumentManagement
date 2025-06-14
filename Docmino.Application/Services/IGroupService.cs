using Docmino.Application.Models;

public interface IGroupService
{
    Task<Result> GetAll(FilterRequest filter);
    Task<Result> GetById(Guid id);
    Task<Result> Add(GroupRequest modelRequest);
    Task<Result> Update(Guid id, GroupRequest modelRequest);
    Task<Result> Delete(Guid id);
}