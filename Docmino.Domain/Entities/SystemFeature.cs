using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class SystemFeature : CountableEntity
{
    public string Name { get; set; }
    public string Code { get; set; }

    public IList<UserFeature> UserFeatures { get; set; }
}
