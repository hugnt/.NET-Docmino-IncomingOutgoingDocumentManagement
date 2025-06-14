using Docmino.Application.Models;
using Docmino.Application.Models.Requests;
using Docmino.Domain.Enums;

namespace Docmino.Application.Services;
public interface IDocumentService
{
    public Task<Result> GetDocumentLookup(DocumentType? documentType);
    public Task<Result> GetReviewerLookup();
    public Task<Result> InitiateConfirmProcess(Guid id);
    public Task<Result> GetPublishDocuments(DocumentFilterRequest filter);
}
