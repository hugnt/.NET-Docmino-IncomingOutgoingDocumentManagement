using Docmino.Application.Models;
using Docmino.Application.Models.Requests;

namespace Docmino.Application.Services;
public interface IConfirmProcessService
{
    public Task<Result> GetUnconfirmedDocuments(ProcessingDocumentFilterRequest filterRequest);
    public Task<Result> GetUserConfirmDocument(Guid documentId);
    public Task<Result> GetConfirmHistory(ProcessingDocumentFilterRequest filterRequest);
    public Task<Result> ApproveDocument(Guid documentId, ApproveDocumentRequest approveRequest);
    public Task<Result> RejectDocument(Guid documentId, RejectDocumentRequest rejectRequest);
}
