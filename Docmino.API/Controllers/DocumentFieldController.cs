using Docmino.API.Filters;
using Docmino.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/document-fields")]
[RoleAuthorize]
public class DocumentFieldController : ApiControllerBase
{
    private readonly IDocumentFieldService _service;

    public DocumentFieldController(IDocumentFieldService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FilterRequest filter)
    {
        var res = await _service.GetAll(filter);
        return ApiResponse(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await _service.GetById(id);
        return ApiResponse(res);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] DocumentFieldRequest modelRequest)
    {
        var res = await _service.Add(modelRequest);
        return ApiResponse(res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DocumentFieldRequest modelRequest)
    {
        var res = await _service.Update(id, modelRequest);
        return ApiResponse(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _service.Delete(id);
        return ApiResponse(res);
    }
}