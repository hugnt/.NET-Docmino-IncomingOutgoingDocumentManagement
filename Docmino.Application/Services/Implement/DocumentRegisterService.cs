using Docmino.Application.Helpers;
using Docmino.Application.Models;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;
using System.Net;

public class DocumentRegisterService : IDocumentRegisterService
{
    private readonly IRepository<DocumentRegister> _repository;
    private readonly IValidator<DocumentRegisterRequest> _validator;

    public DocumentRegisterService(IRepository<DocumentRegister> repository, IValidator<DocumentRegisterRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result> GetAll(FilterRequest filter)
    {
        Expression<Func<DocumentRegister, bool>> queryFilter = x =>
            filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!);

        var res = await _repository.GetByFilterAsync(filter.PageSize, filter.PageNumber, predicate: queryFilter, selectQuery: DocumentRegisterMapping.SelectResponseExpression);
        return FilterResult<List<DocumentRegisterResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }

    public async Task<Result> GetById(Guid id)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id, selectQuery: DocumentRegisterMapping.SelectResponseExpression);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy sổ đăng ký với Id {id}");
        }
        return Result<DocumentRegisterResponse>.SuccessWithBody(selectedEntity);
    }

    public async Task<Result> Add(DocumentRegisterRequest modelRequest)
    {
        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Name == modelRequest.Name.Trim() && x.Year == modelRequest.Year))
        {
            return Result.Error(HttpStatusCode.BadRequest, $"Sổ đăng ký '{modelRequest.Name}' năm {modelRequest.Year} đã tồn tại.");
        }

        var newEntity = modelRequest.ToEntity();
        _repository.Add(newEntity);
        await _repository.SaveChangesAsync();
        return Result.Success(HttpStatusCode.Created, "Tạo sổ đăng ký thành công.");
    }

    public async Task<Result> Update(Guid id, DocumentRegisterRequest modelRequest)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy sổ đăng ký với Id {id}");
        }

        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Id != id && x.Name == modelRequest.Name.Trim() && x.Year == modelRequest.Year))
        {
            return Result.Error(HttpStatusCode.BadRequest, $"Sổ đăng ký '{modelRequest.Name}' năm {modelRequest.Year} đã tồn tại.");
        }

        selectedEntity.MappingFieldFrom(modelRequest);
        _repository.Update(selectedEntity);
        await _repository.SaveChangesAsync();

        return Result.SuccessNoContent();
    }

    public async Task<Result> Delete(Guid id)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy sổ đăng ký với Id {id}");
        }
        _repository.Delete(selectedEntity);
        await _repository.SaveChangesAsync();
        return Result.SuccessNoContent();
    }
}