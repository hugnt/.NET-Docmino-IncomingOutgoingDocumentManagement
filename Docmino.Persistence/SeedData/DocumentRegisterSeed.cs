using Docmino.Domain.Entities;
using Docmino.Domain.Enums;

namespace Docmino.Persistence.SeedData;

public class DocumentRegisterSeed
{
    private static readonly DateTime _defaultTime = new(2025, 01, 01);
    public static IEnumerable<DocumentRegister> DocumentRegisters => new List<DocumentRegister>()
    {
        new DocumentRegister
        {
            Id = Guid.Parse("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"),
            Name = "Sổ đến năm 2025",
            RegisterType = DocumentType.Incomming,
            Year = 2025,
            IsActive = true,
            Description = "Sổ đăng ký văn bản đến năm 2025",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new DocumentRegister
        {
            Id = Guid.Parse("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"),
            Name = "Sổ đi năm 2025",
            RegisterType = DocumentType.Outgoing,
            Year = 2025,
            IsActive = true,
            Description = "Sổ đăng ký văn bản đi năm 2025",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new DocumentRegister
        {
            Id = Guid.Parse("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"),
            Name = "Sổ nội bộ năm 2025",
            RegisterType = DocumentType.InternalIncomming,
            Year = 2025,
            IsActive = true,
            Description = "Sổ đăng ký văn bản nội bộ năm 2025",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new DocumentRegister
        {
            Id = Guid.Parse("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"),
            Name = "Sổ tổng hợp năm 2025",
            RegisterType = DocumentType.None,
            Year = 2025,
            IsActive = true,
            Description = "Sổ tổng hợp đăng ký văn bản năm 2025",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new DocumentRegister
        {
            Id = Guid.Parse("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"),
            Name = "Sổ đặc biệt năm 2025",
            RegisterType = DocumentType.Incomming,
            Year = 2025,
            IsActive = false,
            Description = "Sổ đặc biệt cho văn bản đến (đã lưu trữ)",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        }
    };
}
