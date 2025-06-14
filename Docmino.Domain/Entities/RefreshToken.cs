using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class RefreshToken : Entity
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public string JwtId { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime ExpireAt { get; set; }
}
