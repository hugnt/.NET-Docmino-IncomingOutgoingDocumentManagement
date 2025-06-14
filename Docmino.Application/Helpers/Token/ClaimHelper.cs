using System.Security.Claims;

namespace Docmino.Application.Helpers.Token;
public static class ClaimHelper
{
    public static T? ExtractClaimValue<T>(this IEnumerable<Claim> claims, string claimType, Func<string, T> parser)
    {
        var value = claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        if (value == null) return default;

        try
        {
            return parser(value);
        }
        catch
        {
            return default;
        }
    }


}
