using Docmino.Application.Abstractions.HttpContext;
using Docmino.Application.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Docmino.Infrastructure.HttpContext;
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId =>
       _httpContextAccessor
           .HttpContext?
           .User
           .GetUserId() ??
       throw new ApplicationException("User context is unavailable");

    public string AccessToken =>
        _httpContextAccessor
            .HttpContext?
            .Request
            .Headers["Authorization"]
            .FirstOrDefault()?
            .Split(" ")
            .Last() ??
        throw new ApplicationException("Accesstoken is unavailable");

    public bool IsAuthenticated =>
        _httpContextAccessor
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ??
        throw new ApplicationException("User context is unavailable");


}


internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(ClaimType.UserId);

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId : Guid.Empty;
        //return Guid.TryParse(userId, out Guid parsedUserId) ?
        //    parsedUserId :
        //    throw new ApplicationException("User id is unavailable");
    }
}