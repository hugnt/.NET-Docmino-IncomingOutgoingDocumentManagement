using Docmino.Application.Abstractions.HttpContext;
using Docmino.Application.Helpers;
using Docmino.Application.Models;
using Docmino.Application.Models.Internal;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Docmino.Application.Services.Implement;
public class DocumentFileService : IDocumentFileService
{
    private readonly IRepository<DocumentFile> _documentFileRepository;
    private readonly IUserContext _userContext;
    private readonly IMemoryCache _memoryCache;
    public DocumentFileService(IUserContext userContext,
                            IMemoryCache memoryCache,
                            IRepository<DocumentFile> documentFileRepository)
    {

        _userContext = userContext;
        _memoryCache = memoryCache;
        _documentFileRepository = documentFileRepository;
    }

    public async Task<Result> GetFileUrl(Guid documentFileId)
    {
        var selectedFile = await _documentFileRepository
            .FirstOrDefaultAsync(x => x.Id == documentFileId, selectQuery: x => new DocumentFileAdapter
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

        if (selectedFile == null)
        {
            return Result.Error(HttpStatusCode.NotFound, "File is not existed");
        }

        var selectedUrl = selectedFile.OriginalFileUrl;
        if (selectedFile.CurrentProcessStepNumber > 1 && !selectedFile.ProcessSignHistories.IsEmpty())
        {
            selectedUrl = selectedFile.ProcessSignHistories!.OrderByDescending(psh => psh.SignedAt).First(x => x.StepNumber <= selectedFile.CurrentProcessStepNumber).ProcessFileUrl;
        }

        return Result<string>.SuccessWithBody(selectedUrl);
    }

}
