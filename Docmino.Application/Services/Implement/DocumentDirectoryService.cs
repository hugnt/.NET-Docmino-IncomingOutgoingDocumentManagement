using Docmino.Application.Common.Messages;
using Docmino.Application.Helpers;
using Docmino.Application.Models;
using Docmino.Application.Models.Mappings;
using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using Docmino.Domain.Enums;
using FluentValidation;
using System.Linq.Expressions;
using System.Net;

namespace Docmino.Application.Services.Implement;
public class DocumentDirectoryService : IDocumentDirectoryService
{
    private readonly IValidator<DocumentDirectoryRequest> _documentDirectoryValidator;
    private readonly IRepository<DocumentDirectory> _documentDirectoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DocumentDirectoryService(IRepository<DocumentDirectory> documentDirectoryRepository, IValidator<DocumentDirectoryRequest> documentDirectoryValidator, IUnitOfWork unitOfWork)
    {
        _documentDirectoryRepository = documentDirectoryRepository;
        _documentDirectoryValidator = documentDirectoryValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> GetDirectoryTree()
    {
        var directories = (await _documentDirectoryRepository
                                    .GetAllAsync(x => !x.IsDeleted,
                                                selectQuery: DocumentDirectoryMapping.SelectTreeItemResponseExpression,
                                                 orderByExpressions: [
                                                     (OrderBy: x => x.CreatedAt, IsDescending: true)
                                                 ])
                          ).ToList();

        foreach (var dir in directories.Where(x => x.Type == DirectoryType.Sheft))
        {
            dir.DocumentCount = directories.Where(x => x.Type == DirectoryType.Box && x.ParentDirectoryId == dir.Id).Sum(x => x.DocumentCount);
        }

        foreach (var dir in directories.Where(x => x.Type == DirectoryType.Inventory))
        {
            dir.DocumentCount = directories.Where(x => x.Type == DirectoryType.Sheft && x.ParentDirectoryId == dir.Id).Sum(x => x.DocumentCount);
        }

        return Result<List<DirectoryTreeItemResponse>>.SuccessWithBody(directories);
    }

    public async Task<Result> GetAll(DocumentDirectoryFilterRequest filter)
    {
        Expression<Func<DocumentDirectory, bool>> queryFilter = x =>
                                        (filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!))
                                        && (filter.Type == null || x.Type == filter.Type)
                                        && (filter.ParentDirectoryId == null || x.ParentDirectoryId == filter.ParentDirectoryId);



        var res = await _documentDirectoryRepository.GetByFilterAsync(filter.PageSize, filter.PageNumber,
                                                      predicate: queryFilter,
                                                      selectQuery: DocumentDirectoryMapping.SelectResponseExpression,
                                                      orderByExpressions: [(
                                                            OrderBy: x => x.CreatedAt, IsDescending: true)
                                                      ]);
        return FilterResult<List<DocumentDirectoryResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }

    public async Task<Result> GetDetail(Guid id)
    {
        var directory = await _documentDirectoryRepository.FirstOrDefaultAsync(x => x.Id == id, DocumentDirectoryMapping.SelectResponseExpression);
        if (directory == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Thư mục"));
        }
        return Result<DocumentDirectoryResponse>.SuccessWithBody(directory);
    }

    public async Task<Result> Add(DocumentDirectoryRequest directoryRequest)
    {
        //Validate
        var validateResult = _documentDirectoryValidator.Validate(directoryRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }

        //Check
        var checkResult = await CheckAddDirectory(directoryRequest);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        var directory = directoryRequest.ToEntity();
        _documentDirectoryRepository.Add(directory);

        await _unitOfWork.SaveChangesAsync();
        return Result.Success(HttpStatusCode.Created, SuccessMessage.CreatedSuccessfully("Thư mục"));
    }

    public async Task<Result> Update(Guid id, DocumentDirectoryRequest directoryRequest)
    {
        //Validate
        var validateResult = _documentDirectoryValidator.Validate(directoryRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }

        //Check
        var checkResult = await CheckUpdateDirectory(id, directoryRequest);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        var seletedDirectory = await _documentDirectoryRepository.FirstAsync(x => x.Id == id);
        seletedDirectory.MappingFieldFrom(directoryRequest);

        _documentDirectoryRepository.Update(seletedDirectory);
        await _unitOfWork.SaveChangesAsync();
        return Result.SuccessNoContent();
    }

    public async Task<Result> Delete(Guid id)
    {
        var directory = await _documentDirectoryRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (directory == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound("Thư mục"));
        }
        await DeleteFolder(id);
        await _unitOfWork.SaveChangesAsync();
        return Result.SuccessNoContent();
    }

    public async Task DeleteFolder(Guid id)
    {
        var selectedDirectory = await _documentDirectoryRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedDirectory == null) return;
        _documentDirectoryRepository.Delete(selectedDirectory);

        var childDirectory = await _documentDirectoryRepository.GetAllAsync(x => x.ParentDirectoryId == id);
        if (childDirectory == null) return;
        foreach (var child in childDirectory)
        {
            await DeleteFolder(child.Id);
        }

    }


    private async Task<Checker> CheckAddDirectory(DocumentDirectoryRequest directoryRequest)
    {
        if (directoryRequest.ParentDirectoryId == null && directoryRequest.Type != DirectoryType.Inventory)
        {
            return Checker.Error("Cần chọn thư mục cha cho thư mục này");
        }
        if (await _documentDirectoryRepository.AnyAsync(x => x.ParentDirectoryId == directoryRequest.ParentDirectoryId
                                                    && x.Name.ToLower() == directoryRequest.Name.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(directoryRequest.Name, "Tên thư mục"));
        }
        return Checker.Success();
    }

    private async Task<Checker> CheckUpdateDirectory(Guid id, DocumentDirectoryRequest directoryRequest)
    {
        if (!await _documentDirectoryRepository.AnyAsync(x => x.Id == id))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound("Thư mục"), HttpStatusCode.NotFound);
        }
        if (directoryRequest.ParentDirectoryId == null && directoryRequest.Type != DirectoryType.Inventory)
        {
            return Checker.Error("Cần chọn thư mục cha cho thư mục này");
        }
        if (await _documentDirectoryRepository.AnyAsync(x => x.Id != id && x.ParentDirectoryId == directoryRequest.ParentDirectoryId && x.Name.ToLower() == directoryRequest.Name.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(directoryRequest.Name, "Tên thư mục"));
        }
        return Checker.Success();
    }
}
