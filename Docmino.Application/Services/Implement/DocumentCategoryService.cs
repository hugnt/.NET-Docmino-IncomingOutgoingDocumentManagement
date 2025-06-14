using Docmino.Application.Common.Messages;
using Docmino.Application.Helpers;
using Docmino.Application.Models;
using Docmino.Application.Models.Mappings;
using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;
using System.Net;

namespace Docmino.Application.Services.Implement;
public class DocumentCategoryService : IDocumentCategoryService
{

    private readonly IRepository<DocumentCategory> _repository;
    private readonly IValidator<DocumentCategoryRequest> _validator;
    public DocumentCategoryService(IRepository<DocumentCategory> repository,
                                   IValidator<DocumentCategoryRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result> GetAll(FilterRequest filter)
    {
        Expression<Func<DocumentCategory, bool>> queryFilter = x =>
            filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!);
        var res = await _repository.GetByFilterAsync(filter.PageSize, filter.PageNumber, predicate: queryFilter, selectQuery: DocumentCategoryMapping.SelectResponseExpression);
        return FilterResult<List<DocumentCategoryResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }
    public async Task<Result> GetById(int id)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id, selectQuery: DocumentCategoryMapping.SelectResponseExpression);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Category"));
        }
        return Result<DocumentCategoryResponse>.SuccessWithBody(selectedEntity);
    }

    public async Task<Result> Add(DocumentCategoryRequest modelRequest)
    {
        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Name == modelRequest.Name.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ObjectExisted(modelRequest.Name, "Category"));
        }

        var newEntity = modelRequest.ToEntity();

        _repository.Add(newEntity);
        await _repository.SaveChangesAsync();
        return Result.Success(HttpStatusCode.Created, SuccessMessage.CreatedSuccessfully("Category"));
    }

    public async Task<Result> Update(int id, DocumentCategoryRequest modelRequest)
    {
        var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Category"));
        }

        var validateResult = _validator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        if (await _repository.AnyAsync(x => x.Id != id && x.Name == modelRequest.Name.Trim()))
        {
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ObjectExisted(modelRequest.Name, "Category"));
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
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Category"));
        }
        _repository.Delete(selectedEntity);
        await _repository.SaveChangesAsync();
        return Result.SuccessNoContent();
    }
}
