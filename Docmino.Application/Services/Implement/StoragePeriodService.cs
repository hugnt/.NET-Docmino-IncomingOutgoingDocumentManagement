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
public class StoragePeriodService : IStoragePeriodService
{

    private readonly IRepository<StoragePeriod> _repository;
    private readonly IValidator<StoragePeriodRequest> _validator;
    public StoragePeriodService(IRepository<StoragePeriod> repository,
                                   IValidator<StoragePeriodRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result> Lookup()
    {
        var res = await _repository.GetAllAsync(selectQuery: x => new Lookup<int>()
        {
            Id = x.Id,
            Name = x.Name
        });
        return Result<List<Lookup<int>>>.SuccessWithBody(res.ToList());
    }

    public async Task<Result> GetAll(FilterRequest filter)
    {
        Expression<Func<StoragePeriod, bool>> queryFilter = x =>
                filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!);
        var res = await _repository.GetByFilterAsync(filter.PageSize, filter.PageNumber, predicate: queryFilter, selectQuery: StoragePeriodMapping.SelectResponseExpression);
        return FilterResult<List<StoragePeriodResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }
    public async Task<Result> GetById(int id)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id, selectQuery: StoragePeriodMapping.SelectResponseExpression);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Thời hạn bảo quản"));
        }
        return Result<StoragePeriodResponse>.SuccessWithBody(selectedEntity);
    }

    public async Task<Result> Add(StoragePeriodRequest modelRequest)
    {
        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Name == modelRequest.Name.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ObjectExisted(modelRequest.Name, "Tên thời hạn bảo quản"));
        }

        var newEntity = modelRequest.ToEntity();

        _repository.Add(newEntity);
        await _repository.SaveChangesAsync();
        return Result.Success(HttpStatusCode.Created, SuccessMessage.CreatedSuccessfully("Thời hạn bảo quản"));
    }

    public async Task<Result> Update(int id, StoragePeriodRequest modelRequest)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Thời hạn bảo quản"));
        }

        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Id != id && x.Name == modelRequest.Name.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ObjectExisted(modelRequest.Name, "Tên thời hạn bảo quản"));
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
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Thời hạn bảo quản"));
        }
        _repository.Delete(selectedEntity);
        await _repository.SaveChangesAsync();
        return Result.SuccessNoContent();
    }
}
