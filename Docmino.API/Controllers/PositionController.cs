using Docmino.API.Filters;
using Docmino.Application.Models;
using Docmino.Application.Models.Requests;
using Docmino.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/positions")]
[RoleAuthorize]
public class PositionController : ApiControllerBase
{
    private readonly IPositionService _service;

    public PositionController(IPositionService service)
    {
        _service = service;
    }

    [HttpGet("look-up")]
    public async Task<IActionResult> Lookup()
    {
        var res = await _service.Lookup();
        return ApiResponse(res);
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
    public async Task<IActionResult> Add([FromBody] PositionRequest modelRequest)
    {
        var res = await _service.Add(modelRequest);
        return ApiResponse(res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PositionRequest modelRequest)
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