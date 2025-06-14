using Docmino.Application.Helpers;
using Docmino.Application.Models;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;
using System.Net;

public class DocumentFieldService : IDocumentFieldService
{
    private readonly IRepository<DocumentField> _repository;
    private readonly IValidator<DocumentFieldRequest> _validator;

    public DocumentFieldService(IRepository<DocumentField> repository, IValidator<DocumentFieldRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result> GetAll(FilterRequest filter)
    {
        Expression<Func<DocumentField, bool>> queryFilter = x =>
            filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!) || x.Code.Contains(filter.SearchValue!);

        var res = await _repository.GetByFilterAsync(filter.PageSize, filter.PageNumber, predicate: queryFilter, selectQuery: DocumentFieldMapping.SelectResponseExpression);
        return FilterResult<List<DocumentFieldResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }

    public async Task<Result> GetById(int id)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id, selectQuery: DocumentFieldMapping.SelectResponseExpression);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy lĩnh vực với Id {id}");
        }
        return Result<DocumentFieldResponse>.SuccessWithBody(selectedEntity);
    }

    public async Task<Result> Add(DocumentFieldRequest modelRequest)
    {
        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Name == modelRequest.Name.Trim() || x.Code == modelRequest.Code.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, $"Lĩnh vực '{modelRequest.Name}' hoặc mã '{modelRequest.Code}' đã tồn tại.");
        }

        var newEntity = modelRequest.ToEntity();
        _repository.Add(newEntity);
        await _repository.SaveChangesAsync();
        return Result.Success(HttpStatusCode.Created, "Tạo lĩnh vực thành công.");
    }

    public async Task<Result> Update(int id, DocumentFieldRequest modelRequest)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy lĩnh vực với Id {id}");
        }

        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Id != id && (x.Name == modelRequest.Name.Trim() || x.Code == modelRequest.Code.Trim())))
        {
            return Result.Error(HttpStatusCode.BadRequest, $"Lĩnh vực '{modelRequest.Name}' hoặc mã '{modelRequest.Code}' đã tồn tại.");
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
            return Result.Error(HttpStatusCode.NotFound, $"Không tìm thấy lĩnh vực với Id {id}");
        }
        _repository.Delete(selectedEntity);
        await _repository.SaveChangesAsync();
        return Result.SuccessNoContent();
    }
}