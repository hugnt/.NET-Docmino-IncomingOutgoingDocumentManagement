using Docmino.Application.Models;
using Docmino.Application.Models.Requests;
using Microsoft.AspNetCore.Http;

namespace Docmino.Application.Services;
public interface IInternalDocumentService
{
    Task<Result> GetAll(InternalDocumentFilterRequest filter);
    Task<Result> GetById(Guid id);
    Task<Result> AddIncomingDocument(InternalIncomingDocumentRequest modelRequest, List<IFormFile>? files);
    Task<Result> UpdateIncomingDocument(Guid id, InternalIncomingDocumentRequest modelRequest, List<IFormFile>? files);
    Task<Result> AddOutgoingDocument(InternalOutgoingDocumentRequest modelRequest, List<IFormFile>? files);
    Task<Result> UpdateOutgoingDocument(Guid id, InternalOutgoingDocumentRequest modelRequest, List<IFormFile>? files);
    Task<Result> Delete(Guid id);
}
