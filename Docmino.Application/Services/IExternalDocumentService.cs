using Docmino.Application.Models;
using Docmino.Application.Models.Requests;
using Microsoft.AspNetCore.Http;

namespace Docmino.Application.Services;
public interface IExternalDocumentService
{
    Task<Result> GetAll(ExternalDocumentFilterRequest filter);
    Task<Result> GetById(Guid id);
    Task<Result> AddIncomingDocument(ExternalIncomingDocumentRequest modelRequest, List<IFormFile>? files);
    Task<Result> UpdateIncomingDocument(Guid id, ExternalIncomingDocumentRequest modelRequest, List<IFormFile>? files);
    Task<Result> AddOutgoingDocument(ExternalOutgoingDocumentRequest modelRequest, List<IFormFile>? files);
    Task<Result> UpdateOutgoingDocument(Guid id, ExternalOutgoingDocumentRequest modelRequest, List<IFormFile>? files);
    Task<Result> Delete(Guid id);
}
