using Docmino.API.Filters;
using Docmino.Application.Common.Enums;
using Docmino.Application.Models.Requests;
using Docmino.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/document-directory")]
[RoleAuthorize]
public class DocumentDirectoryController : ApiControllerBase
{
    private readonly IDocumentDirectoryService _service;

    public DocumentDirectoryController(IDocumentDirectoryService service)
    {
        _service = service;
    }

    [HttpGet("tree")]
    public async Task<IActionResult> GetDirectoryTree()
    {
        var res = await _service.GetDirectoryTree();
        return ApiResponse(res);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DocumentDirectoryFilterRequest filter)
    {
        var res = await _service.GetAll(filter);
        return ApiResponse(res);
    }


    [HttpPost]
    [RoleAuthorize(RolePolicy.ClericalAssistant, RolePolicy.Admin)]
    public async Task<IActionResult> Add([FromBody] DocumentDirectoryRequest modelRequest)
    {
        var res = await _service.Add(modelRequest);
        return ApiResponse(res);
    }

    [HttpPut("{id}")]
    [RoleAuthorize(RolePolicy.ClericalAssistant, RolePolicy.Admin)]
    public async Task<IActionResult> Update(Guid id, [FromBody] DocumentDirectoryRequest modelRequest)
    {
        var res = await _service.Update(id, modelRequest);
        return ApiResponse(res);
    }

    [HttpDelete("{id}")]
    [RoleAuthorize(RolePolicy.ClericalAssistant, RolePolicy.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var res = await _service.Delete(id);
        return ApiResponse(res);
    }
}