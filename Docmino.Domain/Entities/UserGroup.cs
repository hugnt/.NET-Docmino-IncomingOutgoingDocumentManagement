using Docmino.Domain.Enums;

namespace Docmino.Domain.Entities;
public class UserGroup
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public GroupRole GroupRole { get; set; }
    public User User { get; set; }
    public Group Group { get; set; }
}
