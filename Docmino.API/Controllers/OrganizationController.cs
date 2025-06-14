using Docmino.API.Filters;
using Docmino.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/organizations")]
[RoleAuthorize]
public class OrganizationController : ApiControllerBase
{
    private readonly IOrganizationService _service;

    public OrganizationController(IOrganizationService service)
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
    public async Task<IActionResult> Add([FromBody] OrganizationRequest modelRequest)
    {
        var res = await _service.Add(modelRequest);
        return ApiResponse(res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrganizationRequest modelRequest)
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