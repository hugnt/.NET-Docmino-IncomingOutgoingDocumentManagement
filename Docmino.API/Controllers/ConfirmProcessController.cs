using Docmino.API.Filters;
using Docmino.Application.Common.Enums;
using Docmino.Application.Models.Requests;
using Docmino.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/confirm-process")]
[RoleAuthorize]
public class ConfirmProcessController : ApiControllerBase
{
    private readonly IConfirmProcessService _service;

    public ConfirmProcessController(IConfirmProcessService service)
    {
        _service = service;
    }

    [HttpGet("documents")]
    public async Task<IActionResult> GetUnconfirmedDocuments([FromQuery] ProcessingDocumentFilterRequest filterRequest)
    {
        var res = await _service.GetUnconfirmedDocuments(filterRequest);
        return ApiResponse(res);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetConfirmHistory([FromQuery] ProcessingDocumentFilterRequest filterRequest)
    {
        var res = await _service.GetConfirmHistory(filterRequest);
        return ApiResponse(res);
    }

    [HttpGet("documents/{documentId:guid}/user-confirm-infomation")]
    public async Task<IActionResult> GetUserConfirmDocument(Guid documentId)
    {
        var res = await _service.GetUserConfirmDocument(documentId);
        return ApiResponse(res);
    }

    [HttpPost("documents/{documentId:guid}/approve")]
    [RoleAuthorize(RolePolicy.Approver)]
    public async Task<IActionResult> ApproveDocument(Guid documentId, [FromForm] ApproveDocumentRequest approveDocumentRequest)
    {
        var res = await _service.ApproveDocument(documentId, approveDocumentRequest);
        return ApiResponse(res);
    }

    [HttpPost("documents/{documentId:guid}/reject")]
    [RoleAuthorize(RolePolicy.Approver)]
    public async Task<IActionResult> RejectDocument(Guid documentId, [FromBody] RejectDocumentRequest approveDocumentRequest)
    {
        var res = await _service.RejectDocument(documentId, approveDocumentRequest);
        return ApiResponse(res);
    }


}
