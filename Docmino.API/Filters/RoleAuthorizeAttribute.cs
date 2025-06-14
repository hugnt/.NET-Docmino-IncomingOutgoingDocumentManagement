using Docmino.Application.Common.Enums;
using Docmino.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Docmino.API.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly List<RolePolicy> _requiredRoles;

    public RoleAuthorizeAttribute(params RolePolicy[] roleSets)
    {
        _requiredRoles = roleSets.Length > 0 ? [.. roleSets] : [RolePolicy.Approver, RolePolicy.ClericalAssistant, RolePolicy.Approver];
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
        {
            return;
        }

        if (context.HttpContext.Items[ClaimType.Role] is not RolePolicy roleReceived)
        {
            context.Result = new JsonResult(Result.Error(HttpStatusCode.Unauthorized, "Token invalid or missing"))
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return;
        }

        if (roleReceived != RolePolicy.Admin && !_requiredRoles.Contains(roleReceived))
        {
            context.Result = new JsonResult(Result.Error(HttpStatusCode.Forbidden, "You do not have permission to perform this action"))
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
            return;
        }
    }
}