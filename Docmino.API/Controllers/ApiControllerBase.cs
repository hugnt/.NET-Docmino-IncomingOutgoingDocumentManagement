using Docmino.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Docmino.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    protected IActionResult ApiResponse(Result result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            _ => StatusCode((int)result.StatusCode, result)
        };
    }
}