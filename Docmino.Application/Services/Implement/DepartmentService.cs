using Docmino.Application.Common.Messages;
using Docmino.Application.Helpers;
using Docmino.Application.Models;
using Docmino.Application.Models.Requests;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;
using System.Net;

namespace Docmino.Application.Services.Implement;
public class DepartmentService : IDepartmentService
{

    private readonly IRepository<Department> _repository;
    private readonly IValidator<DepartmentRequest> _validator;
    public DepartmentService(IRepository<Department> repository,
                                   IValidator<DepartmentRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result> Lookup()
    {
        var res = await _repository.GetAllAsync(selectQuery: x => new Lookup<int>
        {
            Id = x.Id,
            Name = x.Name
        });
        return Result<List<Lookup<int>>>.SuccessWithBody(res.ToList());
    }

    public async Task<Result> GetAll(FilterRequest filter)
    {
        Expression<Func<Department, bool>> queryFilter = x =>
                                        (filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!));

        var res = await _repository.GetByFilterAsync(filter.PageSize, filter.PageNumber, predicate: queryFilter, selectQuery: DepartmentMapping.SelectResponseExpression);
        return FilterResult<List<DepartmentResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }
    public async Task<Result> GetById(int id)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id, selectQuery: DepartmentMapping.SelectResponseExpression);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Phòng ban"));
        }
        return Result<DepartmentResponse>.SuccessWithBody(selectedEntity);
    }

    public async Task<Result> Add(DepartmentRequest modelRequest)
    {
        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Name == modelRequest.Name.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ObjectExisted(modelRequest.Name, "Phòng ban"));
        }

        var newEntity = modelRequest.ToEntity();

        _repository.Add(newEntity);
        await _repository.SaveChangesAsync();
        return Result.Success(HttpStatusCode.Created, SuccessMessage.CreatedSuccessfully("Phòng ban"));
    }

    public async Task<Result> Update(int id, DepartmentRequest modelRequest)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Phòng ban"));
        }

        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Id != id && x.Name == modelRequest.Name.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ObjectExisted(modelRequest.Name, "Phòng ban"));
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
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Phòng ban"));
        }
        if (await _repository.AnyAsync(x => x.ProcessDetails != null || x.Documents != null))
        {
            return Result.ErrorWithMessage(ErrorMessage.ObjectIsInOtherProcess("Phòng ban", selectedEntity.Name));
        }
        _repository.Delete(selectedEntity);
        await _repository.SaveChangesAsync();
        return Result.SuccessNoContent();
    }
}
