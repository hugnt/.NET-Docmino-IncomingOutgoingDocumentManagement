using Docmino.API.Filters;
using Docmino.Application.Common.Enums;
using Docmino.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/statistics")]
[RoleAuthorize(RolePolicy.ClericalAssistant, RolePolicy.Admin)]
public class StatisticController : ApiControllerBase
{
    private readonly IStatisticService _service;

    public StatisticController(IStatisticService service)
    {
        _service = service;
    }


    [HttpGet("entity-counters")]
    public async Task<IActionResult> GetEntityCountersAsync()
    {
        var res = await _service.GetEntityCountersAsync();
        return ApiResponse(res);
    }

    [HttpGet("document-status")]
    public async Task<IActionResult> GetDocumentStatusCountersAsync()
    {
        var res = await _service.GetDocumentStatusCountersAsync();
        return ApiResponse(res);
    }

    [HttpGet("monthly-document-statistic")]
    public async Task<IActionResult> GetMonthlyDocumentStatisticsAsync()
    {
        var res = await _service.GetMonthlyDocumentStatisticsAsync();
        return ApiResponse(res);
    }

}