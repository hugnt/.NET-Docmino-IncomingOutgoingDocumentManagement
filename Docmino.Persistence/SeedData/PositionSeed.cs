using Docmino.Domain.Entities;

namespace Docmino.Persistence.SeedData;

public class PositionSeed
{
    public static IEnumerable<Position> Positions => new List<Position>()
    {
        new Position { Id = 1, Name = "Giám đốc", DepartmentId = 1 },
        new Position { Id = 2, Name = "Trợ lý giám đốc", DepartmentId = 1 },

        new Position { Id = 3, Name = "Trưởng phòng kinh doanh", DepartmentId = 2 },
        new Position { Id = 4, Name = "Nhân viên kinh doanh", DepartmentId = 2 },

        new Position { Id = 5, Name = "Kế toán trưởng", DepartmentId = 3 },
        new Position { Id = 6, Name = "Kế toán viên", DepartmentId = 3 },

        new Position { Id = 7, Name = "Trưởng phòng nhân sự", DepartmentId = 4 },
        new Position { Id = 8, Name = "Chuyên viên nhân sự", DepartmentId = 4 },

        new Position { Id = 9, Name = "Nhân viên hành chính", DepartmentId = 5 },

        new Position { Id = 10, Name = "Trưởng phòng IT", DepartmentId = 6 },
        new Position { Id = 11, Name = "Lập trình viên", DepartmentId = 6 },
        new Position { Id = 12, Name = "Kỹ thuật hệ thống", DepartmentId = 6 },

        new Position { Id = 13, Name = "Quản lý dự án", DepartmentId = 7 },

        new Position { Id = 14, Name = "Nhân viên marketing", DepartmentId = 8 },

        new Position { Id = 15, Name = "Chuyên viên đào tạo", DepartmentId = 9 },

        new Position { Id = 16, Name = "Kiểm soát viên nội bộ", DepartmentId = 10 }
    };
}
