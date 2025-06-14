using Docmino.Application.Helpers;
using Docmino.Application.Models;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;
using System.Net;

public class OrganizationService : IOrganizationService
{
    private readonly IRepository<Organization> _repository;
    private readonly IValidator<OrganizationRequest> _validator;

    public OrganizationService(IRepository<Organization> repository, IValidator<OrganizationRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result> GetAll(FilterRequest filter)
    {
        Expression<Func<Organization, bool>> queryFilter = x =>
            filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!);

        var res = await _repository.GetByFilterAsync(filter.PageSize, filter.PageNumber, predicate: queryFilter, selectQuery: OrganizationMapping.SelectResponseExpression);
        return FilterResult<List<OrganizationResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }

    public async Task<Result> GetById(int id)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id, selectQuery: OrganizationMapping.SelectResponseExpression);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy tổ chức với Id {id}");
        }
        return Result<OrganizationResponse>.SuccessWithBody(selectedEntity);
    }

    public async Task<Result> Add(OrganizationRequest modelRequest)
    {
        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Name == modelRequest.Name.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, $"Tổ chức '{modelRequest.Name}' đã tồn tại.");
        }

        var newEntity = modelRequest.ToEntity();
        _repository.Add(newEntity);
        await _repository.SaveChangesAsync();
        return Result.Success(HttpStatusCode.Created, "Tạo tổ chức thành công.");
    }

    public async Task<Result> Update(int id, OrganizationRequest modelRequest)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy tổ chức với Id {id}");
        }

        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Id != id && x.Name == modelRequest.Name.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, $"Tổ chức '{modelRequest.Name}' đã tồn tại.");
        }

        selectedEntity.MappingFieldFrom(modelRequest);
        _repository.Update(selectedEntity);
        await _repository.SaveChangesAsync();

        return Result.SuccessNoContent();
    }

    public async Task<Result> Delete(int id)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy tổ chức với Id {id}");
        }
        _repository.Delete(selectedEntity);
        await _repository.SaveChangesAsync();
        return Result.SuccessNoContent();
    }
}