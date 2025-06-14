using Docmino.Domain.Entities;

namespace Docmino.Persistence.SeedData;

public class DepartmentSeed
{
    private static readonly DateTime _defaultTime = new(2025, 01, 01);
    public static IEnumerable<Department> Departments => new List<Department>()
    {
        new Department { Id = 1, Name = "Lãnh đạo", Id0 = 1, Code = "LD", CreatedAt = _defaultTime, UpdatedAt = _defaultTime },
        new Department { Id = 2, Name = "Phòng Kinh Doanh", Id0 = 2, Code = "KD", CreatedAt = _defaultTime, UpdatedAt = _defaultTime },
        new Department { Id = 3, Name = "Phòng Kế Toán", Id0 = 3, Code = "KT", CreatedAt = _defaultTime, UpdatedAt = _defaultTime },
        new Department { Id = 4, Name = "Phòng Nhân sự", Id0 = 4, Code = "NS", CreatedAt = _defaultTime, UpdatedAt = _defaultTime },
        new Department { Id = 5, Name = "Phòng Hành Chính", Id0 = 5, Code = "HC", CreatedAt = _defaultTime, UpdatedAt = _defaultTime },
        new Department { Id = 6, Name = "Phòng IT", Id0 = 6, Code = "IT", CreatedAt = _defaultTime, UpdatedAt = _defaultTime },
        new Department { Id = 7, Name = "Phòng Quản Lý Dự Án", Id0 = 7, Code = "DA", CreatedAt = _defaultTime, UpdatedAt = _defaultTime },
        new Department { Id = 8, Name = "Phòng Marketing", Id0 = 8, Code = "MKT", CreatedAt = _defaultTime, UpdatedAt = _defaultTime },
        new Department { Id = 9, Name = "Phòng Đào Tạo", Id0 = 9, Code = "DT", CreatedAt = _defaultTime, UpdatedAt = _defaultTime },
        new Department { Id = 10, Name = "Phòng Kiểm Soát Nội Bộ", Id0 = 10, Code = "KS", CreatedAt = _defaultTime, UpdatedAt = _defaultTime }
    };
}
