using Docmino.Domain.Entities;

namespace Docmino.Persistence.SeedData;

public class RoleSeed
{
    public static IEnumerable<Role> Roles => new List<Role>()
    {
        new(){ Id = 1, Name="Admin", Code="ADMIN" },
        new(){ Id = 2, Name="Chuyên viên văn thư", Code="CLERICAL_ASSISTANT" },
        new(){ Id = 3, Name="Người kí & duyệt", Code="APPROVER" },
    };
}
