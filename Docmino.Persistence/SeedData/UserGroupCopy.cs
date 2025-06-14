using Docmino.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docmino.Persistence.SeedData;

public class UserGroupSeed
{
    public static IEnumerable<UserGroup> UserGroups => new List<UserGroup>()
    {
        new UserGroup()
        {
            UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            GroupId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        },
        new UserGroup()
        {
            UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            GroupId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
        },
        new UserGroup()
        {
            UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            GroupId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
        },
        new UserGroup()
        {
            UserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            GroupId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        },
        new UserGroup()
        {
            UserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            GroupId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
        },
        new UserGroup()
        {
            UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            GroupId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
        },
        new UserGroup()
        {
            UserId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            GroupId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
        },
        new UserGroup()
        {
            UserId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            GroupId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
        },
        new UserGroup()
        {
            UserId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            GroupId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
        },

    };
}
