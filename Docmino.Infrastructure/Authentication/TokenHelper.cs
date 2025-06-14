using System.Security.Cryptography;

namespace Docmino.Infrastructure.Authentication;
public static class TokenHelper
{
    public static string GenerateRefreshToken()
    {
        var random = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(random);
        return Convert.ToBase64String(random);
    }
}