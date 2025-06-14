using Docmino.API.Filters;
using Docmino.Application.Common.Enums;
using Docmino.Application.Models.Requests;
using Docmino.Application.Services;
using Docmino.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/documents")]
[RoleAuthorize]
public class DocumentController : ApiControllerBase
{
    private readonly IDocumentService _service;

    public DocumentController(IDocumentService service)
    {
        _service = service;
    }

    [HttpGet("document-lookup")]
    public async Task<IActionResult> GetDocumentLookup([FromQuery] DocumentType? documentType)
    {
        var res = await _service.GetDocumentLookup(documentType);
        return ApiResponse(res);
    }

    [HttpGet("publish")]
    public async Task<IActionResult> GetPublishDocuments([FromQuery] DocumentFilterRequest filter)
    {
        var res = await _service.GetPublishDocuments(filter);
        return ApiResponse(res);
    }

    [HttpGet("reviewer-lookup")]
    public async Task<IActionResult> GetReviewerLookup()
    {
        var res = await _service.GetReviewerLookup();
        return ApiResponse(res);
    }


    [HttpPatch("{id:guid}/initiate-process")]
    [RoleAuthorize(RolePolicy.ClericalAssistant)]
    public async Task<IActionResult> InitiateConfirmProcess(Guid Id)
    {
        var res = await _service.InitiateConfirmProcess(Id);
        return ApiResponse(res);
    }

}
