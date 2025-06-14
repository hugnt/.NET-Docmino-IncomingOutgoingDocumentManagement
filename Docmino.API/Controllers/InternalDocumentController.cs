using Docmino.API.Filters;
using Docmino.Application.Common.Enums;
using Docmino.Application.Models;
using Docmino.Application.Models.Requests;
using Docmino.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Docmino.API.Controllers;

[Route("api/internal-documents")]
[RoleAuthorize]
public class InternalDocumentController : ApiControllerBase
{
    private readonly IInternalDocumentService _service;

    public InternalDocumentController(IInternalDocumentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] InternalDocumentFilterRequest filter)
    {
        var res = await _service.GetAll(filter);
        return ApiResponse(res);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var res = await _service.GetById(id);
        return ApiResponse(res);
    }

    [HttpPost("incoming")]
    [RoleAuthorize(RolePolicy.ClericalAssistant)]
    public async Task<IActionResult> AddIncomingDocument([FromForm] string documentRequest, [FromForm] List<IFormFile>? fileRequests)
    {
        var parsedRequest = JsonConvert.DeserializeObject<InternalIncomingDocumentRequest>(documentRequest);
        if (parsedRequest == null) return ApiResponse(Result.Error(HttpStatusCode.BadRequest, "Document must be not null!"));
        var res = await _service.AddIncomingDocument(parsedRequest, fileRequests);
        return ApiResponse(res);
    }

    [HttpPut("incoming/{id}")]
    [RoleAuthorize(RolePolicy.ClericalAssistant)]
    public async Task<IActionResult> UpdateIncomingDocument(Guid id, [FromForm] string documentRequest, [FromForm] List<IFormFile>? fileRequests)
    {
        var parsedRequest = JsonConvert.DeserializeObject<InternalIncomingDocumentRequest>(documentRequest);
        if (parsedRequest == null) return ApiResponse(Result.Error(HttpStatusCode.BadRequest, "Document must be not null!"));
        var res = await _service.UpdateIncomingDocument(id, parsedRequest, fileRequests);
        return ApiResponse(res);
    }

    [HttpPost("outgoing")]
    [RoleAuthorize(RolePolicy.ClericalAssistant)]
    public async Task<IActionResult> AddOutgoingDocument([FromForm] string documentRequest, [FromForm] List<IFormFile>? fileRequests)
    {
        var parsedRequest = JsonConvert.DeserializeObject<InternalOutgoingDocumentRequest>(documentRequest);
        if (parsedRequest == null) return ApiResponse(Result.Error(HttpStatusCode.BadRequest, "Document must be not null!"));
        var res = await _service.AddOutgoingDocument(parsedRequest, fileRequests);
        return ApiResponse(res);
    }

    [HttpPut("outgoing/{id}")]
    [RoleAuthorize(RolePolicy.ClericalAssistant)]
    public async Task<IActionResult> UpdateOutgoingDocument(Guid id, [FromForm] string documentRequest, [FromForm] List<IFormFile>? fileRequests)
    {
        var parsedRequest = JsonConvert.DeserializeObject<InternalOutgoingDocumentRequest>(documentRequest);
        if (parsedRequest == null) return ApiResponse(Result.Error(HttpStatusCode.BadRequest, "Document must be not null!"));
        var res = await _service.UpdateOutgoingDocument(id, parsedRequest, fileRequests);
        return ApiResponse(res);
    }

    [HttpDelete("{id}")]
    [RoleAuthorize(RolePolicy.ClericalAssistant)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var res = await _service.Delete(id);
        return ApiResponse(res);
    }

}
