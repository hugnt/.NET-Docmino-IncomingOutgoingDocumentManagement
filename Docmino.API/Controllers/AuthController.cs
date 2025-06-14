using Docmino.API.Filters;
using Docmino.Application.Models.Requests;
using Docmino.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;


[Route("api/auth")]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var res = await _authService.Login(loginRequest);
        return ApiResponse(res);
    }


    [RoleAuthorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest logoutRequest)
    {
        var res = await _authService.Logout(logoutRequest);
        return ApiResponse(res);
    }

    [HttpPost("extend-session")]
    public async Task<IActionResult> ExtendSession([FromBody] ExtendSessionRequest extendSessionRequest)
    {
        var res = await _authService.ExtendSession(extendSessionRequest);
        return ApiResponse(res);
    }

    [RoleAuthorize]
    [HttpGet("get-current-context")]
    public async Task<IActionResult> GetCurrentUserContext()
    {
        var res = await _authService.GetCurrentUserContext();
        return ApiResponse(res);
    }
}
