using Docmino.Domain.Enums;

public class GroupResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<MemberInfoResponse>? Members { get; set; }
}

public class MemberInfoResponse
{
    public Guid UserId { get; set; }
    public GroupRole GroupRole { get; set; }
    public string Fullname { get; set; }
    public string DepartmentName { get; set; }
}