using Docmino.API.Filters;
using Docmino.Application.Common.Enums;
using Docmino.Application.Models;
using Docmino.Application.Models.Requests;
using Docmino.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docmino.API.Controllers;

[Route("api/user")]
[RoleAuthorize]
public class UserController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("approver/look-up")]
    [RoleAuthorize(RolePolicy.Admin, RolePolicy.ClericalAssistant)]
    public async Task<IActionResult> LookupApprover()
    {
        var res = await _userService.LookupApprover();
        return ApiResponse(res);
    }

    [HttpGet]
    [RoleAuthorize(RolePolicy.Admin)]
    public async Task<IActionResult> GetAll([FromQuery] FilterRequest filter)
    {
        var res = await _userService.GetAll(filter);
        return ApiResponse(res);
    }

    [HttpGet("{id}")]
    [RoleAuthorize(RolePolicy.Admin)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var res = await _userService.GetById(id);
        return ApiResponse(res);
    }

    [HttpPost]
    [RoleAuthorize(RolePolicy.Admin)]
    public async Task<IActionResult> Add([FromBody] AddUserRequest modelRequest)
    {
        var res = await _userService.Add(modelRequest);
        return ApiResponse(res);
    }

    [HttpPut("{id}")]
    [RoleAuthorize(RolePolicy.Admin)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest modelRequest)
    {
        var res = await _userService.Update(id, modelRequest);
        return ApiResponse(res);
    }

    [HttpPatch("update-rights")]
    [RoleAuthorize(RolePolicy.Admin)]
    public async Task<IActionResult> UpdateRights([FromBody] UpdateUserRightRequest modelRequest)
    {
        var res = await _userService.UpdateRights(modelRequest);
        return ApiResponse(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var res = await _userService.Delete(id);
        return ApiResponse(res);
    }


    [HttpPatch("image-signature")]
    public async Task<IActionResult> UpdateImageSignature([FromForm] UpdateImageSignatureRequest updateImage)
    {
        var res = await _userService.UpdateImageSignature(updateImage);
        return ApiResponse(res);
    }


    [HttpPatch("digital-certificate")]
    public async Task<IActionResult> UpdateDigitalCertificate([FromForm] UpdateDigitalCertificateRequest updateDigitalCertificate)
    {
        var res = await _userService.UpdateDigitalCertificate(updateDigitalCertificate);
        return ApiResponse(res);
    }

    [HttpPatch("email")]
    public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailRequest emailRequest)
    {
        var res = await _userService.UpdateEmail(emailRequest);
        return ApiResponse(res);
    }

    [HttpPatch("change-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest updatePasswordRequest)
    {
        var res = await _userService.UpdatePassword(updatePasswordRequest);
        return ApiResponse(res);
    }
}
