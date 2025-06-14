using Docmino.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docmino.Persistence.SeedData;

public class GroupSeed
{
    public static IEnumerable<Group> Groups => new List<Group>()
    {
        new Group
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Admin",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            IsDeleted = false
        },
        new Group
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "Manager",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            IsDeleted = false
        },
        new Group
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "HR",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"),
            UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"),
            IsDeleted = false
        },
        new Group
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Name = "IT",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"),
            UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"),
            IsDeleted = false
        },
        new Group
        {
            Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            Name = "Finance",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
            UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
            IsDeleted = false
        },
        new Group
        {
            Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
            Name = "Guest",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000006"),
            UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000006"),
            IsDeleted = false
        }
    };
}
