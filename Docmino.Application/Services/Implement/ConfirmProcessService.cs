using Docmino.Application.Abstractions.Email;
using Docmino.Application.Abstractions.FileSignature;
using Docmino.Application.Abstractions.FileStorage;
using Docmino.Application.Abstractions.HostedServices;
using Docmino.Application.Abstractions.HttpContext;
using Docmino.Application.Common.Messages;
using Docmino.Application.Helpers;
using Docmino.Application.Helpers.Files;
using Docmino.Application.Models;
using Docmino.Application.Models.External;
using Docmino.Application.Models.External.Email;
using Docmino.Application.Models.Internal;
using Docmino.Application.Models.Mappings;
using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using Docmino.Domain.Enums;
using System.Linq.Expressions;
using System.Net;

namespace Docmino.Application.Services.Implement;
public class ConfirmProcessService : IConfirmProcessService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IRepository<DocumentFile> _documentFileRepository;
    private readonly IRepository<ProcessDetail> _processDetailRepository;
    private readonly IRepository<ConfirmProcess> _confirmProcessRepository;
    private readonly IRepository<ProcessHistory> _processHistoryRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUserContext _userContext;
    private readonly IFileSignatureService _fileSignatureService;
    private readonly IFileStorageService _fileStorageService;
    private readonly IEmailService _emailService;
    private readonly IBackgroundTaskQueue<Func<IServiceProvider, CancellationToken, ValueTask>> _mailQueue;
    private readonly IUnitOfWork _unitOfWork;
    public ConfirmProcessService(IRepository<ConfirmProcess> confirmProcessRepository,
                                 IDocumentRepository documentRepository,
                                 IUserContext userContext,
                                 IRepository<ProcessDetail> processDetailRepository,
                                 IRepository<User> userRepository,
                                 IFileSignatureService fileSignatureService,
                                 IFileStorageService fileStorageService,
                                 IUnitOfWork unitOfWork,
                                 IRepository<ProcessHistory> processHistoryRepository,
                                 IRepository<DocumentFile> documentFileRepository,
                                 IBackgroundTaskQueue<Func<IServiceProvider, CancellationToken, ValueTask>> mailQueue,
                                 IEmailService emailService)
    {
        _confirmProcessRepository = confirmProcessRepository;
        _documentRepository = documentRepository;
        _userContext = userContext;
        _processDetailRepository = processDetailRepository;
        _userRepository = userRepository;
        _fileSignatureService = fileSignatureService;
        _fileStorageService = fileStorageService;
        _unitOfWork = unitOfWork;
        _processHistoryRepository = processHistoryRepository;
        _documentFileRepository = documentFileRepository;
        _mailQueue = mailQueue;
        _emailService = emailService;
    }

    public async Task<Result> GetUnconfirmedDocuments(ProcessingDocumentFilterRequest filter)
    {
        var userId = _userContext.UserId;
        var userReviewer = await _userRepository.FirstAsync(x => x.Id == userId,
                                                        selectQuery: x => new ReviewerModel
                                                        {
                                                            UserId = x.Id,
                                                            GroupIds = x.UserGroups.Select(g => g.GroupId).ToList(),
                                                            PositionId = x.PositionId,
                                                            DepartmentId = x.Position.DepartmentId
                                                        });

        Expression<Func<ProcessDetail, bool>> queryFilter = x => ((x.ReviewerUserId != null && x.ReviewerUserId == userId)
                                                                || (x.ReviewerGroupId != null && userReviewer.GroupIds != null && userReviewer.GroupIds.Contains(x.ReviewerGroupId.Value))
                                                                || (x.ReviewerPositionId != null && x.ReviewerPositionId == userReviewer.PositionId)
                                                                || (x.ReviewerDepartmentId != null && x.ReviewerDepartmentId == userReviewer.DepartmentId))
                                                                && (x.StepNumber == x.Process.CurrentStepNumber)
                                                                && (x.Process.Status == ProcessStatus.InProcess)
                                                                && (filter.StartDate == null || x.DateStart >= filter.StartDate)
                                                                && (filter.EndDate == null || x.DateEnd <= filter.EndDate)
                                                                && (filter.SearchValue.IsEmpty() || x.Process.Document.Name.Contains(filter.SearchValue!))
                                                                && (filter.DocumentType == null || x.Process.Document.DocumentRegister.RegisterType == filter.DocumentType);

        var res = await _processDetailRepository.GetByFilterAsync(pageSize: filter.PageSize,
                                                            pageNumber: filter.PageNumber,
                                                            selectQuery: ProcessDetailMapping.SelectDocumentResponseExpression,
                                                            predicate: queryFilter);


        return FilterResult<List<ProcessingDocumentResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }

    public async Task<Result> GetUserConfirmDocument(Guid documentId)
    {
        if (!await _documentRepository.AnyAsync(x => x.Id == documentId))
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(documentId, "Văn bản"));
        }
        var userProcess = await GetUserProcess(documentId, _userContext.UserId);
        return Result<ProcessDetailResponse>.SuccessWithBody(userProcess);
    }

    public async Task<Result> GetConfirmHistory(ProcessingDocumentFilterRequest filterRequest)
    {
        var userId = _userContext.UserId;
        Expression<Func<ProcessHistory, bool>> queryFilter = x => x.UserReviewerId == userId
                                                                && (filterRequest.StartDate == null || x.CreatedAt.Date >= filterRequest.StartDate.Value.ToDateTime(TimeOnly.MinValue))
                                                                && (filterRequest.EndDate == null || x.CreatedAt <= filterRequest.EndDate.Value.ToDateTime(TimeOnly.MaxValue))
                                                                && (filterRequest.SearchValue.IsEmpty() || x.Process.Document.Name.Contains(filterRequest.SearchValue!))
                                                                && (filterRequest.DocumentType == null || x.Process.Document.DocumentRegister.RegisterType == filterRequest.DocumentType);

        var res = await _processHistoryRepository.GetByFilterAsync(pageSize: filterRequest.PageSize,
                                                            pageNumber: filterRequest.PageNumber,
                                                            selectQuery: x => new SignedDocumentResponse()
                                                            {
                                                                Id = x.Process.Document.Id,
                                                                Name = x.Process.Document.Name,
                                                                DocumentRegisterName = x.Process.Document.DocumentRegister.Name,
                                                                DocumentType = x.Process.Document.DocumentRegister.RegisterType,
                                                                CurrentStepNumber = x.CurrentStepNumber,
                                                                NextStepNumber = x.NextStepNumber,
                                                                CodeNotation = x.Process.Document.CodeNotation ?? string.Empty,
                                                                IsApproved = x.Process.Status == ProcessStatus.Completed || x.NextStepNumber > x.CurrentStepNumber,
                                                                ActionName = x.ActionName ?? string.Empty,
                                                                ApprovedAt = x.CreatedAt
                                                            },
                                                            predicate: queryFilter,
                                                            orderByExpressions: [(
                                                                OrderBy: x => x.CreatedAt, IsDescending: true)
                                                            ]);

        return FilterResult<List<SignedDocumentResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }

    public async Task<Result> ApproveDocument(Guid documentId, ApproveDocumentRequest approveRequest)
    {
        var userId = _userContext.UserId;
        var selectedUser = await _userRepository.FirstAsync(x => x.Id == userId, UserMapping.SelectResponseExpression);
        var userProcess = await GetUserProcess(documentId, userId);
        var checkResult = await CheckApproveDocument(documentId, approveRequest, userProcess, selectedUser);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        var confirmProcess = await _confirmProcessRepository.FirstAsync(x => x.DocumentId == documentId, selectQuery: ConfirmProcessMapping.SelectLookupExpression);
        var processHistory = userProcess!.ToProcessHistoryEntity(confirmProcess.Id);
        processHistory.UserReviewerId = userId;
        processHistory.NextStepNumber = confirmProcess.NextStepNumber;
        processHistory.Comment = approveRequest.Comment;
        processHistory.ProcessSignHistories = new List<ProcessSignHistory>();

        var taskAddSignatureAndUploadFile = new List<Task>();
        foreach (var fileSign in approveRequest.ImageSignatures ?? [])
        {
            var selectedProcessDetail = userProcess!.SignDetails!.First(x => x.FileId == fileSign.FileId);
            var selectedFileUrl = await GetFileUrl(fileSign.FileId);
            var signatureModel = new SignatureModel
            {
                FileUrl = selectedFileUrl,
                ImageFile = fileSign.Image,
                PosX = (float)selectedProcessDetail.PosX,
                PosY = (float)selectedProcessDetail.PosY,
                Width = (float)selectedProcessDetail.SignZoneWidth,
                Height = (float)selectedProcessDetail.SignZoneHeight,
                PageNumber = selectedProcessDetail.SignPage,
            };
            var etension = Path.GetExtension(selectedProcessDetail.FileName);
            var fileNameNoExt = Path.GetFileNameWithoutExtension(selectedProcessDetail.FileName);
            var signedFileName = FileHelper.FormatFilename(fileNameNoExt + "_signed_" + confirmProcess.Id + "_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + etension);
            var signedFileUrl = _fileStorageService.GetFileUrl(signedFileName, true);

            if (userProcess!.SignType == SignType.DigitalSignature)
            {
                var digitalSignatureModel = signatureModel.ToDigitalSignature(selectedUser!.DigitalCertificate!);
                digitalSignatureModel.SignatureKeyField = $"sign_process_step_{confirmProcess.CurrentStepNumber}_{DateTimeOffset.UtcNow.Microsecond}";
                digitalSignatureModel.SignerName = selectedUser.Fullname;
                digitalSignatureModel.SignerEmail = selectedUser.Email ?? string.Empty;
                digitalSignatureModel.SignerDepartment = selectedUser?.DepartmentName ?? "";
                digitalSignatureModel.Password = approveRequest.DigitalSignaturePassword!;
                var fileSigned = await _fileSignatureService.AddDigitalSignature(digitalSignatureModel);
                var task = _fileStorageService.UploadFileAsync(fileSigned, signedFileName, true);
                taskAddSignatureAndUploadFile.Add(task);
            }
            else
            {
                var fileSigned = await _fileSignatureService.AddImageToPDF(signatureModel);
                var task = _fileStorageService.UploadFileAsync(fileSigned, signedFileName, true);
                taskAddSignatureAndUploadFile.Add(task);
            }

            var processSignHistory = new ProcessSignHistory
            {
                ProcessHistoryId = processHistory.Id,
                OriginalFileId = fileSign.FileId,
                FileUrl = signedFileUrl,
            };
            processHistory.ProcessSignHistories.Add(processSignHistory);
        }

        try
        {
            await _confirmProcessRepository.BeginTransactionAsync();
            var selectedProcess = await _confirmProcessRepository.FirstAsync(x => x.DocumentId == documentId);
            selectedProcess.CurrentStepNumber = confirmProcess.NextStepNumber;
            if (confirmProcess.NextStepNumber == processHistory.CurrentStepNumber)
            {
                var selectedDocument = await _documentRepository.FirstAsync(x => x.Id == documentId);
                selectedDocument.DocumentStatus = DocumentStatus.Published;
                _documentRepository.Update(selectedDocument);

                selectedProcess.Status = ProcessStatus.Completed;
            }

            _confirmProcessRepository.Update(selectedProcess);
            _processHistoryRepository.Add(processHistory);

            await _unitOfWork.SaveChangesAsync();
            if (taskAddSignatureAndUploadFile.Count > 0)
            {
                await Task.WhenAll(taskAddSignatureAndUploadFile);
            }

            if (selectedProcess.Status != ProcessStatus.Completed)
            {
                await SendApprovalEmailAsync(confirmProcess.NextStepNumber, selectedProcess.Id);
            }

            await _confirmProcessRepository.CommitAsync();
            return Result.SuccessWithMessage("Duyệt văn bản thành công.");
        }
        catch (Exception)
        {
            await _confirmProcessRepository.RollbackAsync();
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ServerError());
        }
    }

    public async Task<Result> RejectDocument(Guid documentId, RejectDocumentRequest rejectRequest)
    {
        var userId = _userContext.UserId;
        var userProcess = await GetUserProcess(documentId, userId);
        var checkResult = await CheckRejectDocument(documentId, rejectRequest, userProcess);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        var selectedUser = await _userRepository.FirstAsync(x => x.Id == userId);
        var confirmProcess = await _confirmProcessRepository
            .FirstAsync(x => x.DocumentId == documentId, selectQuery: ConfirmProcessMapping.SelectLookupExpression);

        var processHistory = userProcess!.ToProcessHistoryEntity(confirmProcess.Id);
        processHistory.UserReviewerId = userId;
        processHistory.Comment = rejectRequest.Comment;
        processHistory.NextStepNumber = userProcess!.VetoRight
            ? (rejectRequest.RollbackStep != null ? (int)rejectRequest.RollbackStep : confirmProcess.PreviousStepNumber)
            : confirmProcess.PreviousStepNumber;

        var processEntity = await _confirmProcessRepository.FirstAsync(x => x.DocumentId == documentId);
        processEntity.CurrentStepNumber = processHistory.NextStepNumber;

        if (confirmProcess.CurrentStepNumber == 1 && processHistory.NextStepNumber == 1)
        {
            var selectedDocument = await _documentRepository.FirstAsync(x => x.Id == documentId);
            selectedDocument.DocumentStatus = DocumentStatus.Cancel;
            _documentRepository.Update(selectedDocument);

            processEntity.Status = ProcessStatus.Cancelled;
        }

        _confirmProcessRepository.Update(processEntity);
        _processHistoryRepository.Add(processHistory);

        if (processEntity.Status != ProcessStatus.Cancelled)
        {
            await SendApprovalEmailAsync(confirmProcess.NextStepNumber, confirmProcess.Id);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.SuccessWithMessage("Từ chối văn bản thành công");
    }


    private async Task<ProcessDetailResponse?> GetUserProcess(Guid documentId, Guid userId)
    {
        var userReviewer = await _userRepository.FirstAsync(x => x.Id == userId,
                                                        selectQuery: x => new ReviewerModel
                                                        {
                                                            UserId = x.Id,
                                                            GroupIds = x.UserGroups.Select(g => g.GroupId).ToList(),
                                                            PositionId = x.PositionId,
                                                            DepartmentId = x.Position.DepartmentId
                                                        });
        Expression<Func<ProcessDetail, bool>> queryFilter = x => ((x.ReviewerUserId != null && x.ReviewerUserId == userId)
                                                                || (x.ReviewerGroupId != null && userReviewer.GroupIds != null && userReviewer.GroupIds.Contains(x.ReviewerGroupId.Value))
                                                                || (x.ReviewerPositionId != null && x.ReviewerPositionId == userReviewer.PositionId)
                                                                || (x.ReviewerDepartmentId != null && x.ReviewerDepartmentId == userReviewer.DepartmentId))
                                                                && (x.StepNumber == x.Process.CurrentStepNumber)
                                                                && (x.Process.Status == ProcessStatus.InProcess)
                                                                && (x.Process.DocumentId == documentId);

        var selectedEntity = await _processDetailRepository.FirstOrDefaultAsync(predicate: queryFilter, selectQuery: ProcessDetailMapping.SelectProcessDetailResponseExpression);
        return selectedEntity;
    }

    private async Task<Checker> CheckApproveDocument(Guid documentId, ApproveDocumentRequest approveRequest, ProcessDetailResponse? userProcess, UserResponse user)
    {
        var selectedDocument = await _documentRepository.FirstOrDefaultAsync(x => x.Id == documentId);
        if (selectedDocument == null)
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(documentId, "Văn bản"), HttpStatusCode.NotFound);
        }
        if (selectedDocument.DocumentStatus != DocumentStatus.InProcess)
        {
            return Checker.Error("Văn bản không ở trạng thái đang xử lý, không thể duyệt văn bản này.");
        }
        if (userProcess == null)
        {
            return Checker.Error(UserMessage.UserDontHavePermissionInThisStep("Duyệt văn bản"));
        }
        if (userProcess.SignType == SignType.Image || userProcess.SignType == SignType.DigitalSignature)
        {
            if (approveRequest.ImageSignatures == null
               || !approveRequest.ImageSignatures.Any()
               || approveRequest.ImageSignatures.Count != userProcess.SignDetails!.Count)
            {
                return Checker.Error("Bạn cần xác nhận đúng số lượng chữ ký để duyệt văn bản này.");
            }

            var lstFilesIdsRequire = userProcess.SignDetails!.Select(x => x.FileId).ToList();
            var lstFilesIdsRequest = approveRequest.ImageSignatures!.Select(x => x.FileId).ToList();
            if (!lstFilesIdsRequire.All(x => lstFilesIdsRequest.Contains(x)))
            {
                return Checker.Error("FIle cần kí không chính xác");
            }
        }

        if (userProcess.SignType == SignType.DigitalSignature)
        {
            if (user.DigitalCertificate.IsEmpty())
            {
                return Checker.Error("Bạn cần có chứng thư số để duyệt văn bản này!");
            }
            if (approveRequest.DigitalSignaturePassword.IsEmpty())
            {
                return Checker.Error("Bạn cần nhập mã pin cho chứng thư số!");
            }

        }

        return Checker.Success();
    }

    private async Task<Checker> CheckRejectDocument(Guid documentId, RejectDocumentRequest rejectRequest, ProcessDetailResponse? userProcess)
    {
        var selectedDocument = await _documentRepository.FirstOrDefaultAsync(x => x.Id == documentId);
        if (selectedDocument == null)
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(documentId, "Văn bản"), HttpStatusCode.NotFound);
        }
        if (selectedDocument.DocumentStatus != DocumentStatus.InProcess)
        {
            return Checker.Error("Văn bản không ở trạng thái đang xử lý, không thể duyệt văn bản này.");
        }
        if (userProcess == null)
        {
            return Checker.Error(UserMessage.UserDontHavePermissionInThisStep("Duyệt văn bản"));
        }
        if (rejectRequest.Comment.IsEmpty())
        {
            return Checker.Error("Bạn cần nhập lý do từ chối để tiếp tục.");
        }
        if (rejectRequest.RollbackStep != null)
        {
            if (rejectRequest.RollbackStep >= userProcess.StepNumber || rejectRequest.RollbackStep <= 0)
            {
                return Checker.Error("Yêu cầu bước duyệt muốn quay lại không hợp lệ");
            }
            if (userProcess.StepNumber - rejectRequest.RollbackStep > 1 && !userProcess.VetoRight)
            {
                return Checker.Error($"Bạn không có quyền phủ quyết để có thể quay lại bước duyệt {rejectRequest.RollbackStep}");
            }
        }

        return Checker.Success();
    }

    private async Task<string> GetFileUrl(Guid documentFileId)
    {
        var selectedFile = await _documentFileRepository
            .FirstAsync(x => x.Id == documentFileId, selectQuery: x => new DocumentFileAdapter
            {
                OriginalFileUrl = x.FileUrl,
                CurrentProcessStepNumber = x.Document.Process != null ? x.Document.Process.CurrentStepNumber : 0,
                ProcessSignHistories = x.ProcessSignHistories != null
                    ? x.ProcessSignHistories.Select(psh => new ProcessSignHistoryAdapter
                    {
                        ProcessFileUrl = psh.FileUrl,
                        StepNumber = psh.ProcessHistory.CurrentStepNumber,
                        SignedAt = psh.ProcessHistory.CreatedAt
                    })
                    : null
            });


        var selectedUrl = selectedFile.OriginalFileUrl;
        if (selectedFile.CurrentProcessStepNumber > 1 && !selectedFile.ProcessSignHistories.IsEmpty())
        {
            selectedUrl = selectedFile.ProcessSignHistories!.OrderByDescending(psh => psh.SignedAt).First(x => x.StepNumber <= selectedFile.CurrentProcessStepNumber).ProcessFileUrl;
        }

        return selectedUrl;
    }

    private async Task SendApprovalEmailAsync(int nextStepNumber, Guid processId)
    {
        // Get process detail for the next step
        var processDetail = await _processDetailRepository
            .FirstAsync(x => x.ProcessId == processId && x.StepNumber == nextStepNumber,
                        selectQuery: ProcessDetailMapping.SelectDocumentProcessDetailResponseExpression);

        // Prepare the email view model
        var emailViewModel = new ApprovalNotificationEmail()
        {
            ReviewerName = processDetail?.ReviewerName ?? "Reviewer",
            DocumentName = processDetail?.DocumentName ?? "",
            CodeNotation = processDetail?.CodeNotation ?? "",
            DocumentTypeName = processDetail?.DocumentType.GetDocumentTypeName() ?? "",
            CurrentStepNumber = processDetail!.StepNumber,
            UrgentPriorityName = processDetail.UrgentPriority.GetUrgentPriorityName(),
            SignTypeName = processDetail.SignType.GetSignTypeName(),
            StartDate = processDetail.DateStart.ToString("dd/MM/yyyy"),
            EndDate = processDetail.DateEnd.ToString("dd/MM/yyyy"),
        };

        var emailReceivers = new List<string>();
        if (processDetail.ReviewerType == ReviewerType.User)
        {
            emailReceivers.Add(processDetail.ReviewerUser?.Email ?? string.Empty);
        }
        else if (processDetail.ReviewerType == ReviewerType.Group)
        {
            emailReceivers = processDetail.ReviewerGroupUser?.Select(x => x.Email).ToList() ?? [];
        }
        else if (processDetail.ReviewerType == ReviewerType.Position)
        {
            emailReceivers = processDetail.ReviewerPositionUser?.Select(x => x.Email).ToList() ?? [];
        }
        else if (processDetail.ReviewerType == ReviewerType.Department)
        {
            emailReceivers = processDetail.ReviewerDepartmentUser?.Select(x => x.Email).ToList() ?? [];
        }

        // Generate email content
        var htmlEmail = await _emailService.GetEmailContentAsync("ApprovalNotificationEmailTemplate", emailViewModel);

        // Send email in background
        await _mailQueue.QueueBackgroundWorkItemAsync(async (sp, cancellationToken) =>
        {
            var emailRequest = new EmailRequest()
            {
                ToEmails = emailReceivers,
                Subject = "[Docmino] Thông báo duyệt văn bản",
                Body = htmlEmail,
            };
            await _emailService.SendEmailAsync(emailRequest);
        });

    }

}
