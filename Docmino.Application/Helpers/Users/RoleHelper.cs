using Docmino.Domain.Entities;
using System.Linq.Expressions;

namespace Docmino.Application.Helpers.Users;
public static class RoleHelper
{
    public static Expression<Func<User, bool>> IsApprover() => x => x.RoleId == 3;
    public static Expression<Func<User, bool>> IsAdmin() => x => x.RoleId == 1;
    public static Expression<Func<User, bool>> IsClericalAssistant() => x => x.RoleId == 2;

    public static bool IsClericalAssistant(this User u) => u.RoleId == 2;
    public static bool IsAdmin(this User u) => u.RoleId == 1;
}