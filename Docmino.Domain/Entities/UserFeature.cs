namespace Docmino.Domain.Entities;
public class UserFeature
{
    public Guid UserId { get; set; }
    public int FeatureId { get; set; }
    public User User { get; set; }
    public SystemFeature Feature { get; set; }
}
