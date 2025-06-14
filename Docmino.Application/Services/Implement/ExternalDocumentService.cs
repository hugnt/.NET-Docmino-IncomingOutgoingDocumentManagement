using Docmino.Application.Abstractions.FileStorage;
using Docmino.Application.Common.Messages;
using Docmino.Application.Helpers;
using Docmino.Application.Helpers.Files;
using Docmino.Application.Models;
using Docmino.Application.Models.Mappings;
using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using Docmino.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Net;

namespace Docmino.Application.Services.Implement;
public class ExternalDocumentService : IExternalDocumentService
{
    private readonly IValidator<ExternalIncomingDocumentRequest> _incomingDocumentValidator;
    private readonly IValidator<ExternalOutgoingDocumentRequest> _outgoingDocumentValidator;
    private readonly IDocumentRepository _documentRepository;
    private readonly IRepository<ConfirmProcess> _confirmProcessRepository;
    private readonly IRepository<ProcessDetail> _processDetailRepository;
    private readonly IRepository<ProcessHistory> _processHistoryRepository;
    private readonly IRepository<DocumentFile> _documentFileRepository;
    private readonly IRepository<ProcessSignDetail> _processSignDetailRepository;
    private readonly IFileStorageService _fileStorageService;

    private readonly IRepository<DocumentCategory> _categoryRepository;
    private readonly IRepository<DocumentRegister> _documentRegisterRepository;
    private readonly IRepository<DocumentField> _documentFieldRepository;
    private readonly IRepository<Organization> _oganizationRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ExternalDocumentService(IUnitOfWork unitOfWork,
                                   IDocumentRepository documentRepository,
                                   IRepository<DocumentFile> documentFileRepository,
                                   IRepository<ConfirmProcess> confirmProcessRepository,
                                   IRepository<ProcessDetail> processDetailRepository,
                                   IRepository<ProcessSignDetail> processSignDetailRepository,
                                   IFileStorageService fileStorageService,
                                   IValidator<ExternalIncomingDocumentRequest> incomingDocumentValidator,
                                   IRepository<DocumentRegister> documentRegisterRepository,
                                   IRepository<DocumentField> documentFieldRepository,
                                   IRepository<Organization> oganizationRepository,
                                   IRepository<DocumentCategory> categoryRepository,
                                   IRepository<ProcessHistory> processHistoryRepository,
                                   IValidator<ExternalOutgoingDocumentRequest> outgoingDocumentValidator)
    {
        _unitOfWork = unitOfWork;
        _documentRepository = documentRepository;
        _documentFileRepository = documentFileRepository;
        _confirmProcessRepository = confirmProcessRepository;
        _processDetailRepository = processDetailRepository;
        _processSignDetailRepository = processSignDetailRepository;
        _fileStorageService = fileStorageService;
        _incomingDocumentValidator = incomingDocumentValidator;
        _documentRegisterRepository = documentRegisterRepository;
        _documentFieldRepository = documentFieldRepository;
        _oganizationRepository = oganizationRepository;
        _categoryRepository = categoryRepository;
        _processHistoryRepository = processHistoryRepository;
        _outgoingDocumentValidator = outgoingDocumentValidator;
    }


    public async Task<Result> GetAll(ExternalDocumentFilterRequest filter)
    {
        Expression<Func<Document, bool>> queryFilter = x =>
                                                (filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!) || (x.ArrivalNumber != null && x.ArrivalNumber.Contains(filter.SearchValue!)))
                                                && (filter.StartDate.IsEmpty() || x.IssuedDate >= filter.StartDate)
                                                && (filter.EndDate.IsEmpty() || x.IssuedDate <= filter.EndDate)
                                                && (filter.DocumentType == null || x.DocumentRegister.RegisterType == filter.DocumentType)
                                                && (filter.ArrivalDates.IsEmpty() || (x.ArrivalDate != null && filter.ArrivalDates!.Contains(x.ArrivalDate.Value)))
                                                && (filter.Categories.IsEmpty() || filter.Categories!.Contains(x.CategoryId))
                                                && (filter.DocumentStatus.IsEmpty() || filter.DocumentStatus!.Contains(x.DocumentStatus))
                                                && (filter.Fields.IsEmpty() || filter.Fields!.Contains(x.FieldId))
                                                && (filter.DocumentRegisters.IsEmpty() || filter.DocumentRegisters!.Contains(x.DocumentRegisterId));

        var res = await _documentRepository.GetByFilterAsync(filter.PageSize, filter.PageNumber,
                                                     predicate: queryFilter,
                                                     selectQuery: ExternalDocumentMapping.SelectResponseExpression);
        return FilterResult<List<ExternalDocumentResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }

    public async Task<Result> GetById(Guid id)
    {
        var selectedEntity = await _documentRepository
                                    .FirstOrDefaultAsync(x => x.Id == id, selectQuery: ExternalDocumentMapping.SelectDetailResponseExpression);
        if (selectedEntity == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Document"));
        }
        if (selectedEntity.ConfirmProcess != null)
        {
            var processDetails = await _processDetailRepository
                                    .GetAllAsync(x => x.ProcessId == selectedEntity.ConfirmProcess.Id,
                                    selectQuery: ProcessDetailMapping.SelectProcessDetailResponseExpression);

            var processHistories = await _processHistoryRepository
                                    .GetAllAsync(x => x.ProcessId == selectedEntity.ConfirmProcess.Id,
                                    selectQuery: ProcessHistoryMapping.SelectProcessHistoryResponseExpression);


            selectedEntity.ConfirmProcess.ProcessDetails = processDetails.OrderBy(x => x.StepNumber).ToList();
            selectedEntity.ConfirmProcess.ProcessHistories = processHistories.OrderByDescending(ph => ph.CreatedAt).ToList();

        }

        return Result<ExternalDocumentDetailResponse>.SuccessWithBody(selectedEntity);
    }

    public async Task<Result> AddIncomingDocument(ExternalIncomingDocumentRequest modelRequest, List<IFormFile>? files)
    {
        //Validate
        var validateResult = _incomingDocumentValidator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }

        //Check
        var checkResult = await CheckAddIncomingDocument(modelRequest);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        //BSL rules
        try
        {
            await _documentRepository.BeginTransactionAsync();

            //Add Document
            var newDocument = modelRequest.ToEntity();
            _documentRepository.Add(newDocument);

            //Add files db
            var documentFiles = (modelRequest.DocumentFiles ?? []).Select(x => x.ToEntity(newDocument.Id)).ToList();
            var uploadFilesTasks = new List<Task>();
            if (documentFiles != null && documentFiles.Count > 0)
            {
                for (int i = 0; i < documentFiles.Count; i++)
                {
                    var file = documentFiles[i];
                    file.DocumentId = newDocument.Id;
                    var etension = Path.GetExtension(file.FileName);
                    var fileNameNoExt = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileName = FileHelper.FormatFilename(fileNameNoExt + "_" + newDocument.Id + etension);
                    uploadFilesTasks.Add(_fileStorageService.UploadFileAsync(files![i], fileName));
                    var fileUrl = _fileStorageService.GetFileUrl(fileName);
                    file.FileUrl = fileUrl;
                    file.FileType = etension;
                    _documentFileRepository.Add(file);
                }
            }

            //Add Process
            if (modelRequest.ConfirmProcess != null)
            {
                var confirmProcess = modelRequest.ConfirmProcess.ToEntity();
                confirmProcess.DocumentId = newDocument.Id;
                _confirmProcessRepository.Add(confirmProcess);

                foreach (var x in modelRequest.ConfirmProcess.ProcessDetails)
                {
                    var processEntity = x.ToEntity(confirmProcess.Id);
                    switch (processEntity.ReviewerType)
                    {
                        case ReviewerType.User:
                            processEntity.ReviewerUserId = x.ReviewerUserId;
                            break;
                        case ReviewerType.Group:
                            processEntity.ReviewerGroupId = x.ReviewerGroupId;
                            break;
                        case ReviewerType.Position:
                            processEntity.ReviewerPositionId = x.ReviewerPositionId;
                            break;
                        case ReviewerType.Department:
                            processEntity.ReviewerDepartmentId = x.ReviewerDepartmentId;
                            break;
                    }
                    _processDetailRepository.Add(processEntity);
                    if (x.SignDetails != null && x.SignDetails.Count > 0)
                    {
                        var signDetails = x.SignDetails.Select(y => y.ToEntity(processEntity.Id, documentFiles![y.FileIndex].Id)).ToList();
                        foreach (var signDetail in signDetails)
                        {
                            _processSignDetailRepository.Add(signDetail);
                        }
                    }
                }

            }
            await _unitOfWork.SaveChangesAsync();
            if (uploadFilesTasks.Count > 0)
            {
                await Task.WhenAll(uploadFilesTasks);
            }
            await _documentRepository.CommitAsync();
            return Result<Guid>.Success(HttpStatusCode.Created, newDocument.Id, SuccessMessage.CreatedSuccessfully("Văn bản"));
        }
        catch (Exception)
        {
            await _documentRepository.RollbackAsync();
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ServerError());
        }
    }

    public async Task<Result> UpdateIncomingDocument(Guid id, ExternalIncomingDocumentRequest modelRequest, List<IFormFile>? files)
    {
        //Validate
        var validateResult = _incomingDocumentValidator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }

        //Check
        var checkResult = await CheckEditIncomingDocument(id, modelRequest);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        //BSL rules
        try
        {
            await _documentRepository.BeginTransactionAsync();

            //Update Document
            var selectedDocument = await _documentRepository.FirstAsync(x => x.Id == id);
            selectedDocument.MappingFieldFrom(modelRequest);
            _documentRepository.Update(selectedDocument);

            //Update existed files
            var lstUpdateFiles = (modelRequest.DocumentFiles ?? []).Where(x => x.Id != null).ToList();
            List<Guid> lstUpdateFileIds = [];
            if (lstUpdateFiles != null)
            {

                foreach (var file in lstUpdateFiles)
                {
                    var updateFile = await _documentFileRepository.FirstAsync(x => x.Id == file.Id);
                    updateFile.MappingFieldFrom(file);
                    _documentFileRepository.Update(updateFile);
                }
                lstUpdateFileIds = lstUpdateFiles.Select(x => x.Id!.Value).ToList();
            }
            var lstOldFileIds = (await _documentFileRepository.GetAllAsync(x => x.DocumentId == id, selectQuery: x => x.Id)).ToList();
            var lstRemoveIds = lstOldFileIds.Except(lstUpdateFileIds);


            //Add files - new file
            var newFiles = (modelRequest.DocumentFiles ?? []).Where(x => x.Id == null).Select(x => x.ToEntity(selectedDocument.Id)).ToList();
            var uploadFilesTasks = new List<Task>();
            if (newFiles != null)
            {
                for (int i = 0; i < newFiles.Count; i++)
                {
                    var file = newFiles[i];
                    file.DocumentId = id;
                    var etension = Path.GetExtension(file.FileName);
                    var fileNameNoExt = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileName = FileHelper.FormatFilename(fileNameNoExt + "_" + selectedDocument.Id + etension);
                    uploadFilesTasks.Add(_fileStorageService.UploadFileAsync(files![i], fileName));
                    var fileUrl = _fileStorageService.GetFileUrl(fileName);
                    file.FileUrl = fileUrl;
                    file.FileType = etension;
                    _documentFileRepository.Add(file);
                    lstUpdateFileIds.Add(file.Id);
                }
            }


            //Update Process
            var currentProcess = await _confirmProcessRepository.FirstOrDefaultAsync(x => x.DocumentId == id);
            if (currentProcess != null)
            {
                _confirmProcessRepository.Delete(currentProcess);
            }
            if (modelRequest.ConfirmProcess != null)
            {
                var confirmProcess = modelRequest.ConfirmProcess.ToEntity();
                confirmProcess.DocumentId = id;
                _confirmProcessRepository.Add(confirmProcess);

                foreach (var x in modelRequest.ConfirmProcess.ProcessDetails)
                {
                    var processEntity = x.ToEntity(confirmProcess.Id);
                    switch (processEntity.ReviewerType)
                    {
                        case ReviewerType.User:
                            processEntity.ReviewerUserId = x.ReviewerUserId;
                            break;
                        case ReviewerType.Group:
                            processEntity.ReviewerGroupId = x.ReviewerGroupId;
                            break;
                        case ReviewerType.Position:
                            processEntity.ReviewerPositionId = x.ReviewerPositionId;
                            break;
                        case ReviewerType.Department:
                            processEntity.ReviewerDepartmentId = x.ReviewerDepartmentId;
                            break;
                    }
                    _processDetailRepository.Add(processEntity);
                    if (x.SignDetails != null && x.SignDetails.Count > 0)
                    {
                        var signDetails = x.SignDetails.Select(y => y.ToEntity(processEntity.Id, lstUpdateFileIds[y.FileIndex]));
                        foreach (var signDetail in signDetails)
                        {
                            _processSignDetailRepository.Add(signDetail);
                        }
                    }
                }

            }
            if (!lstRemoveIds.IsEmpty())
            {
                foreach (var entityId in lstRemoveIds)
                {
                    var selectedEntity = await _documentFileRepository.FirstAsync(x => x.Id == entityId);
                    _documentFileRepository.Delete(selectedEntity);
                }
                //await _documentFileRepository.ExecuteDeleteAsync(x => x.DocumentId == id && lstRemoveIds.Contains(x.Id));
            }

            await _unitOfWork.SaveChangesAsync();
            if (uploadFilesTasks.Count > 0)
            {
                await Task.WhenAll(uploadFilesTasks);
            }
            await _documentRepository.CommitAsync();
        }
        catch (Exception)
        {
            await _documentRepository.RollbackAsync();
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ServerError());
        }
        return Result.Success(HttpStatusCode.NoContent, SuccessMessage.UpdatedSuccessfully("Văn bản"));
    }

    public async Task<Result> AddOutgoingDocument(ExternalOutgoingDocumentRequest modelRequest, List<IFormFile>? files)
    {
        //Validate
        var validateResult = _outgoingDocumentValidator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }

        //Check
        var checkResult = await CheckAddOutgoingDocument(modelRequest);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        //BSL rules
        try
        {
            await _documentRepository.BeginTransactionAsync();

            //Add Document
            var newDocument = modelRequest.ToEntity();
            _documentRepository.Add(newDocument);

            //Add files db
            var documentFiles = (modelRequest.DocumentFiles ?? []).Select(x => x.ToEntity(newDocument.Id)).ToList();
            var uploadFilesTasks = new List<Task>();
            if (documentFiles != null && documentFiles.Count > 0)
            {
                for (int i = 0; i < documentFiles.Count; i++)
                {
                    var file = documentFiles[i];
                    file.DocumentId = newDocument.Id;
                    var etension = Path.GetExtension(file.FileName);
                    var fileNameNoExt = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileName = FileHelper.FormatFilename(fileNameNoExt + "_" + newDocument.Id + etension);
                    uploadFilesTasks.Add(_fileStorageService.UploadFileAsync(files![i], fileName));
                    var fileUrl = _fileStorageService.GetFileUrl(fileName);
                    file.FileUrl = fileUrl;
                    file.FileType = etension;
                    _documentFileRepository.Add(file);
                }
            }

            //Add Process
            if (modelRequest.ConfirmProcess != null)
            {
                var confirmProcess = modelRequest.ConfirmProcess.ToEntity();
                confirmProcess.DocumentId = newDocument.Id;
                _confirmProcessRepository.Add(confirmProcess);

                foreach (var x in modelRequest.ConfirmProcess.ProcessDetails)
                {
                    var processEntity = x.ToEntity(confirmProcess.Id);
                    switch (processEntity.ReviewerType)
                    {
                        case ReviewerType.User:
                            processEntity.ReviewerUserId = x.ReviewerUserId;
                            break;
                        case ReviewerType.Group:
                            processEntity.ReviewerGroupId = x.ReviewerGroupId;
                            break;
                        case ReviewerType.Position:
                            processEntity.ReviewerPositionId = x.ReviewerPositionId;
                            break;
                        case ReviewerType.Department:
                            processEntity.ReviewerDepartmentId = x.ReviewerDepartmentId;
                            break;
                    }
                    _processDetailRepository.Add(processEntity);
                    if (x.SignDetails != null && x.SignDetails.Count > 0)
                    {
                        var signDetails = x.SignDetails.Select(y => y.ToEntity(processEntity.Id, documentFiles![y.FileIndex].Id)).ToList();
                        foreach (var signDetail in signDetails)
                        {
                            _processSignDetailRepository.Add(signDetail);
                        }
                    }
                }

            }
            await _unitOfWork.SaveChangesAsync();
            if (uploadFilesTasks.Count > 0)
            {
                await Task.WhenAll(uploadFilesTasks);
            }
            await _documentRepository.CommitAsync();
            return Result<Guid>.Success(HttpStatusCode.Created, newDocument.Id, SuccessMessage.CreatedSuccessfully("Văn bản"));
        }
        catch (Exception)
        {
            await _documentRepository.RollbackAsync();
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ServerError());
        }
    }

    public async Task<Result> UpdateOutgoingDocument(Guid id, ExternalOutgoingDocumentRequest modelRequest, List<IFormFile>? files)
    {
        //Validate
        var validateResult = _outgoingDocumentValidator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }

        //Check
        var checkResult = await CheckEditOutgoingDocument(id, modelRequest);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        //BSL rules
        try
        {
            await _documentRepository.BeginTransactionAsync();

            //Update Document
            var selectedDocument = await _documentRepository.FirstAsync(x => x.Id == id);
            selectedDocument.MappingFieldFrom(modelRequest);
            _documentRepository.Update(selectedDocument);

            //Update existed files
            var lstUpdateFiles = (modelRequest.DocumentFiles ?? []).Where(x => x.Id != null).ToList();
            List<Guid> lstUpdateFileIds = [];
            if (lstUpdateFiles != null)
            {

                foreach (var file in lstUpdateFiles)
                {
                    var updateFile = await _documentFileRepository.FirstAsync(x => x.Id == file.Id);
                    updateFile.MappingFieldFrom(file);
                    _documentFileRepository.Update(updateFile);
                }
                lstUpdateFileIds = lstUpdateFiles.Select(x => x.Id!.Value).ToList();
            }
            var lstOldFileIds = (await _documentFileRepository.GetAllAsync(x => x.DocumentId == id, selectQuery: x => x.Id)).ToList();
            var lstRemoveIds = lstOldFileIds.Except(lstUpdateFileIds);


            //Add files - new file
            var newFiles = (modelRequest.DocumentFiles ?? []).Where(x => x.Id == null).Select(x => x.ToEntity(selectedDocument.Id)).ToList();
            var uploadFilesTasks = new List<Task>();
            if (newFiles != null)
            {
                for (int i = 0; i < newFiles.Count; i++)
                {
                    var file = newFiles[i];
                    file.DocumentId = id;
                    var etension = Path.GetExtension(file.FileName);
                    var fileNameNoExt = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileName = FileHelper.FormatFilename(fileNameNoExt + "_" + selectedDocument.Id + etension);
                    uploadFilesTasks.Add(_fileStorageService.UploadFileAsync(files![i], fileName));
                    var fileUrl = _fileStorageService.GetFileUrl(fileName);
                    file.FileUrl = fileUrl;
                    file.FileType = etension;
                    _documentFileRepository.Add(file);
                    lstUpdateFileIds.Add(file.Id);
                }
            }


            //Update Process
            var currentProcess = await _confirmProcessRepository.FirstOrDefaultAsync(x => x.DocumentId == id);
            if (currentProcess != null)
            {
                _confirmProcessRepository.Delete(currentProcess);
            }
            if (modelRequest.ConfirmProcess != null)
            {
                var confirmProcess = modelRequest.ConfirmProcess.ToEntity();
                confirmProcess.DocumentId = id;
                _confirmProcessRepository.Add(confirmProcess);

                foreach (var x in modelRequest.ConfirmProcess.ProcessDetails)
                {
                    var processEntity = x.ToEntity(confirmProcess.Id);
                    switch (processEntity.ReviewerType)
                    {
                        case ReviewerType.User:
                            processEntity.ReviewerUserId = x.ReviewerUserId;
                            break;
                        case ReviewerType.Group:
                            processEntity.ReviewerGroupId = x.ReviewerGroupId;
                            break;
                        case ReviewerType.Position:
                            processEntity.ReviewerPositionId = x.ReviewerPositionId;
                            break;
                        case ReviewerType.Department:
                            processEntity.ReviewerDepartmentId = x.ReviewerDepartmentId;
                            break;
                    }
                    _processDetailRepository.Add(processEntity);
                    if (x.SignDetails != null && x.SignDetails.Count > 0)
                    {
                        var signDetails = x.SignDetails.Select(y => y.ToEntity(processEntity.Id, lstUpdateFileIds[y.FileIndex]));
                        foreach (var signDetail in signDetails)
                        {
                            _processSignDetailRepository.Add(signDetail);
                        }
                    }
                }

            }
            if (!lstRemoveIds.IsEmpty())
            {
                foreach (var entityId in lstRemoveIds)
                {
                    var selectedEntity = await _documentFileRepository.FirstAsync(x => x.Id == entityId);
                    _documentFileRepository.Delete(selectedEntity);
                }
                //await _documentFileRepository.ExecuteDeleteAsync(x => x.DocumentId == id && lstRemoveIds.Contains(x.Id));
            }

            await _unitOfWork.SaveChangesAsync();
            if (uploadFilesTasks.Count > 0)
            {
                await Task.WhenAll(uploadFilesTasks);
            }
            await _documentRepository.CommitAsync();
        }
        catch (Exception)
        {
            await _documentRepository.RollbackAsync();
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ServerError());
        }
        return Result.Success(HttpStatusCode.NoContent, SuccessMessage.UpdatedSuccessfully("Văn bản"));
    }

    public async Task<Result> Delete(Guid id)
    {
        var selectedDocument = await _documentRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedDocument == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Văn bản"));
        }
        if (selectedDocument.DocumentStatus != DocumentStatus.Draff)
        {
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ObjectIsInOtherProcess(id, "Văn bản"));
        }
        _documentRepository.Delete(selectedDocument);
        await _unitOfWork.SaveChangesAsync();
        return Result.SuccessNoContent();
    }


    private async Task<Checker> CheckAddIncomingDocument(ExternalIncomingDocumentRequest documentRequest)
    {
        var checkAdd = await CheckExternalDocument(documentRequest);
        if (checkAdd.IsError())
        {
            return checkAdd;
        }
        if (await _documentRepository.AnyAsync(x => x.DocumentRegisterId == documentRequest.DocumentRegisterId && x.ArrivalNumber!.ToLower() == documentRequest.ArrivalNumber.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(documentRequest.ArrivalNumber, "Số đến trong sổ"));
        }
        if (await _documentRepository.AnyAsync(x => x.CodeNotation!.ToLower() == documentRequest.CodeNotation.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(documentRequest.CodeNotation, "Số ký hiệu vb"));
        }
        return Checker.Success();
    }

    private async Task<Checker> CheckEditIncomingDocument(Guid id, ExternalIncomingDocumentRequest documentRequest)
    {
        var selectedDocument = await _documentRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedDocument == null)
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(documentRequest.FieldId, "Văn bản"));
        }
        if (selectedDocument.DocumentStatus != DocumentStatus.Draff)
        {
            return Checker.Error(ErrorMessage.ObjectCanNotBeModified(documentRequest.FieldId, "Văn bản"));
        }
        if (documentRequest.ConfirmProcess != null
           && documentRequest.ConfirmProcess.Id != null
           && !await _confirmProcessRepository.AnyAsync(x => x.Id == documentRequest.ConfirmProcess.Id))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(documentRequest.ConfirmProcess.Id, "Quy trình duyệt"));
        }
        if (documentRequest.DocumentFiles != null)
        {
            var lstUpdateFileIds = documentRequest.DocumentFiles.Where(x => x.Id != null).Select(x => x.Id!.Value);
            if (lstUpdateFileIds != null)
            {
                var lstOldFileIds = await _documentFileRepository.GetAllAsync(x => x.DocumentId == id, selectQuery: x => x.Id);
                if (lstUpdateFileIds.Except(lstOldFileIds).Any())
                {
                    return Checker.Error("Update file lỗi do id gửi lên không hợp lệ");
                }
            }
        }
        var checkAdd = await CheckExternalDocument(documentRequest);
        if (checkAdd.IsError())
        {
            return checkAdd;
        }
        if (await _documentRepository.AnyAsync(x => x.Id != id && x.DocumentRegisterId == documentRequest.DocumentRegisterId && x.ArrivalNumber!.ToLower() == documentRequest.ArrivalNumber.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(documentRequest.ArrivalNumber, "Số đến trong sổ"));
        }
        if (await _documentRepository.AnyAsync(x => x.Id != id && x.CodeNotation!.ToLower() == documentRequest.CodeNotation.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(documentRequest.CodeNotation, "Số ký hiệu vb"));
        }
        return Checker.Success();
    }

    private async Task<Checker> CheckAddOutgoingDocument(ExternalOutgoingDocumentRequest documentRequest)
    {
        var checkAdd = await CheckExternalDocument(documentRequest);
        if (checkAdd.IsError())
        {
            return checkAdd;
        }
        if (await _documentRepository.AnyAsync(x => x.DocumentRegisterId == documentRequest.DocumentRegisterId && x.CodeNumber!.ToLower() == documentRequest.CodeNumber.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(documentRequest.CodeNumber, "Số đi trong sổ"));
        }
        if (await _documentRepository.AnyAsync(x => x.CodeNotation!.ToLower() == documentRequest.CodeNotation.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(documentRequest.CodeNotation, "Số ký hiệu vb"));
        }
        return Checker.Success();
    }

    private async Task<Checker> CheckEditOutgoingDocument(Guid id, ExternalOutgoingDocumentRequest documentRequest)
    {
        var selectedDocument = await _documentRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedDocument == null)
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(documentRequest.FieldId, "Văn bản"));
        }
        if (selectedDocument.DocumentStatus != DocumentStatus.Draff)
        {
            return Checker.Error(ErrorMessage.ObjectCanNotBeModified(documentRequest.FieldId, "Văn bản"));
        }
        if (documentRequest.ConfirmProcess != null
           && documentRequest.ConfirmProcess.Id != null
           && !await _confirmProcessRepository.AnyAsync(x => x.Id == documentRequest.ConfirmProcess.Id))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(documentRequest.ConfirmProcess.Id, "Quy trình duyệt"));
        }
        if (documentRequest.DocumentFiles != null)
        {
            var lstUpdateFileIds = documentRequest.DocumentFiles.Where(x => x.Id != null).Select(x => x.Id!.Value);
            if (lstUpdateFileIds != null)
            {
                var lstOldFileIds = await _documentFileRepository.GetAllAsync(x => x.DocumentId == id, selectQuery: x => x.Id);
                if (lstUpdateFileIds.Except(lstOldFileIds).Any())
                {
                    return Checker.Error("Update file lỗi do id gửi lên không hợp lệ");
                }
            }
        }
        var checkAdd = await CheckExternalDocument(documentRequest);
        if (checkAdd.IsError())
        {
            return checkAdd;
        }
        if (await _documentRepository.AnyAsync(x => x.Id != id && x.DocumentRegisterId == documentRequest.DocumentRegisterId && x.CodeNumber!.ToLower() == documentRequest.CodeNumber.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(documentRequest.CodeNumber, "Số đến trong sổ"));
        }
        if (await _documentRepository.AnyAsync(x => x.Id != id && x.CodeNotation!.ToLower() == documentRequest.CodeNotation.ToLower().Trim()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(documentRequest.CodeNotation, "Số ký hiệu vb"));
        }
        return Checker.Success();
    }

    private async Task<Checker> CheckExternalDocument(ExternalDocumentRequest documentRequest)
    {

        if (documentRequest.ConfirmProcess != null && documentRequest.ConfirmProcess.ProcessDetails != null)
        {
            for (int i = 1; i < documentRequest.ConfirmProcess.ProcessDetails.Count; i++)
            {
                var processDetail = documentRequest.ConfirmProcess.ProcessDetails[i];
                var prevProcessDetail = documentRequest.ConfirmProcess.ProcessDetails[i - 1];
                if (processDetail.StepNumber <= prevProcessDetail.StepNumber)
                {
                    return Checker.Error($"Số thứ tự các bước duyệt chưa đúng!");
                }
                if (processDetail.DateStart < prevProcessDetail.DateStart)
                {
                    return Checker.Error($"Ngày bắt đầu của bước xử lý {processDetail.StepNumber} không được nhỏ hơn ngày bắt đầu của bước xử lý {prevProcessDetail.StepNumber}");
                }
            }
        }
        if (!await _documentRegisterRepository.AnyAsync(x => x.Id == documentRequest.DocumentRegisterId))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(documentRequest.DocumentRegisterId, "Sổ đăng kí vb"));
        }
        if (!await _categoryRepository.AnyAsync(x => x.Id == documentRequest.CategoryId))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(documentRequest.CategoryId, "Danh mục vb"));
        }
        if (!await _documentFieldRepository.AnyAsync(x => x.Id == documentRequest.FieldId))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(documentRequest.FieldId, "Lĩnh vực"));
        }
        if (!await _oganizationRepository.AnyAsync(x => x.Id == documentRequest.OrganizationId))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(documentRequest.OrganizationId, "Tổ chức"));
        }
        return Checker.Success();
    }
}
