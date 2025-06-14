using Docmino.API.Filters;
using Docmino.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/groups")]
[RoleAuthorize]
public class GroupController : ApiControllerBase
{
    private readonly IGroupService _service;

    public GroupController(IGroupService service)
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
    public async Task<IActionResult> GetById(Guid id)
    {
        var res = await _service.GetById(id);
        return ApiResponse(res);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] GroupRequest modelRequest)
    {
        var res = await _service.Add(modelRequest);
        return ApiResponse(res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] GroupRequest modelRequest)
    {
        var res = await _service.Update(id, modelRequest);
        return ApiResponse(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var res = await _service.Delete(id);
        return ApiResponse(res);
    }
}