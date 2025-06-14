using Docmino.API.Filters;
using Docmino.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/document-files")]
[RoleAuthorize]
public class DocumentFileController : ApiControllerBase
{
    private readonly IDocumentFileService _service;

    public DocumentFileController(IDocumentFileService service)
    {
        _service = service;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetNavigationOptions(Guid id)
    {
        var res = await _service.GetFileUrl(id);
        return ApiResponse(res);
    }

}
