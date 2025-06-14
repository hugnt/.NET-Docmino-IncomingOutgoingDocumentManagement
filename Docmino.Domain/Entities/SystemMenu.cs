using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class SystemMenu : CountableEntity
{
	public string Name { get; set; }
    public string Url { get; set; }
    public string Param { get; set; }
    public int Id0 { get; set; }
    public int FeatureId { get; set; }
    public string Description { get; set; }
}
