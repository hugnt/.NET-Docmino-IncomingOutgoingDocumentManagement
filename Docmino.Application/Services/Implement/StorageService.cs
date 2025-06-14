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
public class StorageService : IStorageService
{
    private readonly IValidator<StorageRequest> _storageValidator;
    private readonly IRepository<Storage> _storageRepository;
    private readonly IRepository<StoragePeriod> _storagePeriodRepository;
    private readonly IRepository<DocumentDirectory> _documentDirectoryRepository;
    private readonly IRepository<Document> _documentRepository;
    private readonly IUnitOfWork _unitOfWork;
    public StorageService(IRepository<Storage> storageRepository,
        IValidator<StorageRequest> storageValidator,
        IUnitOfWork unitOfWork,
        IRepository<StoragePeriod> storagePeriodRepository,
        IRepository<DocumentDirectory> documentDirectoryRepository,
        IRepository<Document> documentRepository)
    {
        _storageRepository = storageRepository;
        _storageValidator = storageValidator;
        _unitOfWork = unitOfWork;
        _storagePeriodRepository = storagePeriodRepository;
        _documentDirectoryRepository = documentDirectoryRepository;
        _documentRepository = documentRepository;
    }

    public async Task<Result> GetAll(StorageFilterRequest filter)
    {
        Expression<Func<Storage, bool>> queryFilter = x =>
                                        (filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!) || x.Code.Contains(filter.SearchValue!))
                                        && (filter.BoxId == null || x.DirectoryId == filter.BoxId);



        var res = await _storageRepository.GetByFilterAsync(filter.PageSize, filter.PageNumber,
                                                      predicate: queryFilter,
                                                      selectQuery: StorageMapping.SelectResponseExpression,
                                                      orderByExpressions: [(
                                                            OrderBy: x => x.CreatedAt, IsDescending: true)
                                                      ]);
        return FilterResult<List<StorageResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }

    public async Task<Result> GetDetail(Guid id)
    {
        var directory = await _storageRepository.FirstOrDefaultAsync(x => x.Id == id, StorageMapping.SelectDetailResponseExpression);
        if (directory == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Hồ sơ"));
        }
        return Result<StorageDetailResponse>.SuccessWithBody(directory);
    }

    public async Task<Result> Add(StorageRequest directoryRequest)
    {
        //Validate
        var validateResult = _storageValidator.Validate(directoryRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }

        //Check
        var checkResult = await CheckAdd(directoryRequest);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        var directory = directoryRequest.ToEntity();
        _storageRepository.Add(directory);

        await _unitOfWork.SaveChangesAsync();
        return Result.Success(HttpStatusCode.Created, SuccessMessage.CreatedSuccessfully("Hồ sơ"));
    }

    public async Task<Result> Update(Guid id, StorageRequest directoryRequest)
    {
        //Validate
        var validateResult = _storageValidator.Validate(directoryRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }

        //Check
        var checkResult = await CheckUpdate(id, directoryRequest);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        var seleted = await _storageRepository.FirstAsync(x => x.Id == id);
        seleted.MappingFieldFrom(directoryRequest);

        _storageRepository.Update(seleted);
        await _unitOfWork.SaveChangesAsync();
        return Result.SuccessNoContent();
    }

    public async Task<Result> UpdateDocuments(Guid id, StorageDocumentsRequest modelRequest)
    {
        if (!await _storageRepository.AnyAsync(x => x.Id == id))
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound("Hồ sơ"));
        }
        try
        {
            await _documentRepository.BeginTransactionAsync();
            var queryReset = "UPDATE [Document] SET StorageId = NULL WHERE StorageId = {0}";
            await _documentRepository.ExecuteRawSqlNonQueryAsync(queryReset, id);

            var documentIds = modelRequest.ListDocumentIds;
            if (documentIds?.Any() == true)
            {
                var inClause = string.Join(",", documentIds.Select((_, index) => $"{{{index + 1}}}"));
                var queryUpdate = $"UPDATE [Document] SET StorageId = {{0}} WHERE Id IN ({inClause})";

                var parameters = new object[documentIds.Count + 1];
                parameters[0] = id;
                for (int i = 0; i < documentIds.Count; i++)
                    parameters[i + 1] = documentIds[i];

                await _documentRepository.ExecuteRawSqlNonQueryAsync(queryUpdate, parameters);
            }

            await _documentRepository.CommitAsync();
        }
        catch (Exception)
        {
            await _documentRepository.RollbackAsync();
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ServerError());
        }
        return Result.SuccessNoContent();
    }

    public async Task<Result> Delete(Guid id)
    {
        var directory = await _storageRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (directory == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound("Hồ sơ"));
        }
        _storageRepository.Delete(directory);
        await _unitOfWork.SaveChangesAsync();
        return Result.SuccessNoContent();
    }


    private async Task<Checker> CheckAdd(StorageRequest directoryRequest)
    {
        if (await _storageRepository.AnyAsync(x => x.Code.ToLower() == directoryRequest.Code.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(directoryRequest.Code, "Mã hồ sơ"));
        }
        if (!await _documentDirectoryRepository.AnyAsync(x => x.Id == directoryRequest.BoxId && x.Type == DirectoryType.Box))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(directoryRequest.BoxId, "Hộp lưu trữ"));
        }
        if (!await _storagePeriodRepository.AnyAsync(x => x.Id == directoryRequest.StoragePeriodId))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(directoryRequest.StoragePeriodId, "Thời hạn lưu trữ"));
        }
        return Checker.Success();
    }

    private async Task<Checker> CheckUpdate(Guid id, StorageRequest directoryRequest)
    {
        if (!await _storageRepository.AnyAsync(x => x.Id == id))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound("Hồ sơ"), HttpStatusCode.NotFound);
        }

        if (await _storageRepository.AnyAsync(x => x.Id != id && x.Code.ToLower() == directoryRequest.Code.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(directoryRequest.Code, "Mã hồ sơ"));
        }

        if (!await _documentDirectoryRepository.AnyAsync(x => x.Id == directoryRequest.BoxId && x.Type == DirectoryType.Box))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(directoryRequest.BoxId, "Hộp lưu trữ"));
        }
        if (!await _storagePeriodRepository.AnyAsync(x => x.Id == directoryRequest.StoragePeriodId))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(directoryRequest.StoragePeriodId, "Thời hạn lưu trữ"));
        }
        return Checker.Success();
    }
}
