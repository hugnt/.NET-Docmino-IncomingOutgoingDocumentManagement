using Docmino.Application.Models;

public interface IOrganizationService
{
    Task<Result> GetAll(FilterRequest filter);
    Task<Result> GetById(int id);
    Task<Result> Add(OrganizationRequest modelRequest);
    Task<Result> Update(int id, OrganizationRequest modelRequest);
    Task<Result> Delete(int id);
}