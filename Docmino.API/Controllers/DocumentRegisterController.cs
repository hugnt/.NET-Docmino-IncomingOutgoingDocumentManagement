using Docmino.API.Filters;
using Docmino.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/document-registers")]
[RoleAuthorize]
public class DocumentRegisterController : ApiControllerBase
{
    private readonly IDocumentRegisterService _service;

    public DocumentRegisterController(IDocumentRegisterService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FilterRequest filter)
    {
        var res = await _service.GetAll(filter);
        return ApiResponse(res);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var res = await _service.GetById(id);
        return ApiResponse(res);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] DocumentRegisterRequest modelRequest)
    {
        var res = await _service.Add(modelRequest);
        return ApiResponse(res);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DocumentRegisterRequest modelRequest)
    {
        var res = await _service.Update(id, modelRequest);
        return ApiResponse(res);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var res = await _service.Delete(id);
        return ApiResponse(res);
    }
}