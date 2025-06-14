namespace Docmino.Infrastructure.Authentication;
public class TokenMessage
{
    public const string TokenEmpty = "Token must not be empty!";
    public const string InvalidTokenType = "Invalid token type. Expected JWT.";
    public const string InvalidAlgorithm = "Invalid algorithm: {0}";
    public const string TokenExpired = "Token has expired.";
    public const string InvalidSignatureKey = "Invalid signature key.";
    public const string InvalidSignature = "Token signature is invalid.";
    public const string TokenValidationFailed = "Token validation failed: {0}";
    public const string UnexpectedError = "An unexpected error occurred during token validation.";
}
