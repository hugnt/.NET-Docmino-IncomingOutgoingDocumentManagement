using Docmino.Application.Models.External;
using System.Security.Claims;

namespace Docmino.Application.Abstractions.Authentication;
public interface IAuthenticationService
{
    public TokenModel GenerateTokens(Claim[] claims);
    public TokenValidationModel<ClaimsPrincipal> ValidateAccessToken(string? accessToken);
    public int GetExpirationTimeOfRefreshToken();
    public ClaimsPrincipal GetPrincipalFromToken(string? accessToken);
}
