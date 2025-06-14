using Docmino.Application.Abstractions.Authentication;
using Docmino.Application.Common.Enums;
using Docmino.Application.Helpers.Token;

namespace Docmino.API.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAuthenticationService authenticationService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var validateAccessToken = authenticationService.ValidateAccessToken(token);
        if (validateAccessToken.IsSuccess && validateAccessToken.AttachData?.Claims != null)
        {
            var claims = validateAccessToken.AttachData.Claims;

            context.Items[ClaimType.Role] = claims.ExtractClaimValue(ClaimType.Role, Enum.Parse<RolePolicy>);
            context.Items[ClaimType.UserId] = claims.ExtractClaimValue(ClaimType.UserId, Guid.Parse);
        }
        else
        {
            context.Items[ClaimType.Role] = null;
            context.Items[ClaimType.UserId] = null;
        }
        await _next(context);
    }
}