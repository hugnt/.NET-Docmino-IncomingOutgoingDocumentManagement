using Docmino.API.Filters;
using Docmino.Application.Common.Enums;
using Docmino.Application.Models.Requests;
using Docmino.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/storages")]
[RoleAuthorize]
public class StorageController : ApiControllerBase
{
    private readonly IStorageService _service;

    public StorageController(IStorageService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] StorageFilterRequest filter)
    {
        var res = await _service.GetAll(filter);
        return ApiResponse(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail(Guid id)
    {
        var res = await _service.GetDetail(id);
        return ApiResponse(res);
    }

    [HttpPost]
    [RoleAuthorize(RolePolicy.ClericalAssistant, RolePolicy.Admin)]
    public async Task<IActionResult> Add([FromBody] StorageRequest modelRequest)
    {
        var res = await _service.Add(modelRequest);
        return ApiResponse(res);
    }

    [HttpPut("{id}")]
    [RoleAuthorize(RolePolicy.ClericalAssistant, RolePolicy.Admin)]
    public async Task<IActionResult> Update(Guid id, [FromBody] StorageRequest modelRequest)
    {
        var res = await _service.Update(id, modelRequest);
        return ApiResponse(res);
    }

    [HttpPut("{id}/documents")]
    [RoleAuthorize(RolePolicy.ClericalAssistant, RolePolicy.Admin)]
    public async Task<IActionResult> UpdateDocuments(Guid id, [FromBody] StorageDocumentsRequest modelRequest)
    {
        var res = await _service.UpdateDocuments(id, modelRequest);
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