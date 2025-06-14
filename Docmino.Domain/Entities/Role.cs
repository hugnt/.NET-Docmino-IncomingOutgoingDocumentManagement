using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class Role : CountableEntity
{
    public string Name { get; set; }
	public string Code { get; set; }
    public ICollection<User> Users { get; set; }
}
