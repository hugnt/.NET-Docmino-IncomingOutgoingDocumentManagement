using Docmino.Application.Abstractions.Email;
using Docmino.Application.Abstractions.HostedServices;
using Docmino.Application.Abstractions.HttpContext;
using Docmino.Application.Common.Constants;
using Docmino.Application.Common.Enums;
using Docmino.Application.Common.Messages;
using Docmino.Application.Helpers;
using Docmino.Application.Models;
using Docmino.Application.Models.External;
using Docmino.Application.Models.External.Email;
using Docmino.Application.Models.Mappings;
using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using Docmino.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Docmino.Application.Services.Implement;
public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IRepository<User> _userRepository;

    private readonly IRepository<ProcessDetail> _processDetailRepository;
    private readonly IRepository<ProcessHistory> _processHistory;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IBackgroundTaskQueue<Func<IServiceProvider, CancellationToken, ValueTask>> _mailQueue;
    private readonly IEmailService _emailService;
    private readonly IUserContext _userContext;
    private readonly IMemoryCache _memoryCache;
    public DocumentService(IRepository<User> userRepository,
                            IDocumentRepository documentRepository,
                            IUnitOfWork unitOfWork,
                            IRepository<ProcessHistory> processHistory,
                            IUserContext userContext,
                            IMemoryCache memoryCache,
                            IRepository<ProcessDetail> processDetailRepository,
                            IEmailService emailService,
                            IBackgroundTaskQueue<Func<IServiceProvider, CancellationToken, ValueTask>> mailQueue)
    {
        _userRepository = userRepository;
        _documentRepository = documentRepository;
        _unitOfWork = unitOfWork;
        _processHistory = processHistory;
        _userContext = userContext;
        _memoryCache = memoryCache;
        _processDetailRepository = processDetailRepository;
        _emailService = emailService;
        _mailQueue = mailQueue;
    }

    public async Task<Result> GetDocumentLookup(DocumentType? documentType)
    {
        var cacheKey = StringHelper.ReplacePlaceholders(CacheKeyConstants.LookupDocumentKey, documentType.ToString() ?? "all");
        if (_memoryCache.TryGetValue(cacheKey, out var cachedResponseObj) && cachedResponseObj is DocumentLookupResponse cachedResponse)
        {
            return Result<DocumentLookupResponse>.SuccessWithBody(cachedResponse);
        }

        var res = new DocumentLookupResponse();
        var dates = Enumerable.Range(0, 11)
            .Select(offset => DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-offset)))
            .ToList();
        res.ArrivalDates = res.IssuedDates = dates;

        const string sql = @"
            SELECT CAST(Id AS VARCHAR(50)) AS Id, Name + ' [' + Code + ']' AS Name, 'Category' AS EntityName FROM DocumentCategory
            UNION ALL
            SELECT CAST(Id AS VARCHAR(50)), Name + ' [' + Code + ']', 'Field' FROM DocumentField
            UNION ALL
            SELECT CAST(Id AS VARCHAR(50)), Name, 'Register' FROM DocumentRegister WHERE @p0 IS NULL OR RegisterType = @p0
            UNION ALL
            SELECT CAST(Id AS VARCHAR(50)), Name, 'Organization' FROM Organization
            UNION ALL
            SELECT CAST(Id AS VARCHAR(50)), Name, 'Department' FROM Department
            UNION ALL
            SELECT CAST(Id AS VARCHAR(50)), Fullname, 'Manager' FROM [User] WHERE ProcessManagerRight = @p1
        ";
        var queryResult = await _documentRepository
            .ExecuteRawSqlAsync<UnifiedLookupResult>(
                sql,
                documentType.HasValue ? (int)documentType.Value : DBNull.Value,
                1
            );

        res.Categories = queryResult.ToLookupList<int>("Category");
        res.Fields = queryResult.ToLookupList<int>("Field");
        res.DocumentRegisters = queryResult.ToLookupList<Guid>("Register");
        res.Organizations = queryResult.ToLookupList<int>("Organization");
        res.Departments = queryResult.ToLookupList<int>("Department");
        res.ProcessManagers = queryResult.ToLookupList<Guid>("Manager");


        res.DocumentStatus = EnumHelper.ToLookupList<DocumentStatus>(e => e.GetDocumentStatusName());
        res.SecurePriorities = EnumHelper.ToLookupList<SecurePriority>(e => e.GetSecurePriorityName());
        res.UrgentPriorities = EnumHelper.ToLookupList<UrgentPriority>(e => e.GetUrgentPriorityName());
        res.ProcessTypes = EnumHelper.ToLookupList<ProcessType>(e => e.GetProcessTypeName());
        res.SignTypes = EnumHelper.ToLookupList<SignType>(e => e.GetSignTypeName());
        res.ReviewerTypes = EnumHelper.ToLookupList<ReviewerType>(e => e.GetReviewerTypeName());

        _memoryCache.Set(cacheKey, res, TimeSpan.FromMinutes(3));
        return Result<DocumentLookupResponse>.SuccessWithBody(res);
    }

    public async Task<Result> GetReviewerLookup()
    {
        const string sql = @"
            SELECT CAST(Id AS VARCHAR(50)) AS Id, Fullname AS Name, 'User' AS EntityName 
            FROM [User] 
            WHERE RoleId = @p0
            UNION ALL
            SELECT CAST(Id AS VARCHAR(50)), Name, 'Group' FROM [Group]
            UNION ALL
            SELECT CAST(Id AS VARCHAR(50)), Name, 'Department' FROM Department
            UNION ALL
            SELECT CAST(Id AS VARCHAR(50)), Name, 'Position' FROM Position
        ";

        var lookupResults = await _documentRepository.ExecuteRawSqlAsync<UnifiedLookupResult>(sql, (int)RolePolicy.Approver);
        var res = new ReviewerLookupResponse();
        res.Users = lookupResults.ToLookupList<Guid>("User");
        res.Groups = lookupResults.ToLookupList<Guid>("Group");
        res.Departments = lookupResults.ToLookupList<int>("Department");
        res.Positions = lookupResults.ToLookupList<int>("Position");

        return Result<ReviewerLookupResponse>.SuccessWithBody(res);
    }

    public async Task<Result> InitiateConfirmProcess(Guid id)
    {
        var userId = _userContext.UserId;
        var currentUser = await _userRepository.FirstAsync(x => x.Id == userId);
        var selectedDocument = await _documentRepository.FirstOrDefaultAsync(x => x.Id == id, navigationProperties: [x => x.Process]);
        if (selectedDocument == null)
        {
            return Result.ErrorNotFound(ErrorMessage.ObjectNotFound(id, "Văn bản"));
        }
        if (selectedDocument.DocumentStatus != DocumentStatus.Draff)
        {
            return Result.ErrorWithMessage(ErrorMessage.ObjectIsInOtherProcess(selectedDocument.Name, selectedDocument.DocumentStatus.GetDocumentStatusName()));
        }
        if (selectedDocument.Process == null)
        {
            return Result.ErrorWithMessage("Văn bản chưa được thiết lập quy trình duyệt!");
        }
        selectedDocument.Process.CurrentStepNumber = 1;
        selectedDocument.Process.Status = ProcessStatus.InProcess;
        selectedDocument.DocumentStatus = DocumentStatus.InProcess;

        _documentRepository.Update(selectedDocument);

        var history = new ProcessHistory()
        {
            ProcessId = selectedDocument.Process.Id,
            CurrentStepNumber = 0,
            NextStepNumber = 1,
            ReviewerName = currentUser.Fullname,
            UserReviewerId = userId,
            Comment = "Chuyên viên văn thư trình ký",
            CreatedAt = DateTime.Now,
            ActionName = "Trình ký"

        };
        _processHistory.Add(history);
        await _unitOfWork.SaveChangesAsync();

        await SendApprovalEmailAsync(1, selectedDocument.Process.Id);
        return Result.SuccessNoContent();
    }

    public async Task<Result> GetDetailsDocument(Guid id)
    {
        var selectedDocument = await _documentRepository.FirstOrDefaultAsync(x => x.Id == id, selectQuery: DocumentMapping.SelectDetailProcessingDocumentResponseExpression, navigationProperties: [x => x.Process, x => x.DocumentFiles]);
        if (selectedDocument == null)
        {
            return Result.ErrorNotFound(ErrorMessage.ObjectNotFound(id, "Văn bản"));
        }
        return Result<DocumentDetailResponse>.SuccessWithBody(selectedDocument);
    }

    public async Task<Result> GetPublishDocuments(DocumentFilterRequest filter)
    {
        Expression<Func<Document, bool>> queryFilter = x =>
                            x.DocumentStatus == DocumentStatus.Published
                            && x.StorageId == null
                            && (filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!) || x.CodeNotation!.Contains(filter.SearchValue!))
                            && (filter.DocumentType == null || x.DocumentRegister.RegisterType == filter.DocumentType);

        var res = await _documentRepository.GetByFilterAsync(filter.PageSize, filter.PageNumber,
                                                       predicate: queryFilter,
                                                       selectQuery: DocumentMapping.SelectPublishDocumentResponseExpression,
                                                       orderByExpressions: [(
                                                             OrderBy: x => x.UpdatedAt, IsDescending: true)
                                                       ]);
        return FilterResult<List<PublishDocumentResponse>>.Success(res.Data.ToList(), res.TotalCount);
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
