using Docmino.Domain.Enums;

public class GroupRequest
{
    public string Name { get; set; }
    public List<MemberInfoRequest>? Members { get; set; }
}

public class MemberInfoRequest
{
    public Guid UserId { get; set; }
    public GroupRole GroupRole { get; set; }
}