using Docmino.Application.Models.External.Providers;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Docmino.Infrastructure.Authentication;
public static class TokenValidationConfig
{
    public static TokenValidationParameters GetTokenValidationParameters(TokenSettings tokenSettings)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey))
        };
    }

}