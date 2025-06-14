using Docmino.API.Filters;
using Docmino.Application.Models;
using Docmino.Application.Models.Requests;
using Docmino.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/document-categories")]
[RoleAuthorize]
public class DocumentCategoryController : ApiControllerBase
{
    private readonly IDocumentCategoryService _service;

    public DocumentCategoryController(IDocumentCategoryService service)
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
    public async Task<IActionResult> Add([FromBody] DocumentCategoryRequest modelRequest)
    {
        var res = await _service.Add(modelRequest);
        return ApiResponse(res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DocumentCategoryRequest updatedBookCategory)
    {
        var res = await _service.Update(id, updatedBookCategory);
        return ApiResponse(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _service.Delete(id);
        return ApiResponse(res);
    }
}
