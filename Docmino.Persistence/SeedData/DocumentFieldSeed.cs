using Docmino.Domain.Entities;

namespace Docmino.Persistence.SeedData;

public class DocumentFieldSeed
{
    private static readonly DateTime _defaultTime = new(2025, 01, 01);
    public static IEnumerable<DocumentField> DocumentFields => new List<DocumentField>()
    {
        new DocumentField
        {
            Id = 1,
            Name = "Pháp luật",
            Code = "PL",
            Description = "Tài liệu lĩnh vực pháp luật",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentField
        {
            Id = 2,
            Name = "Y tế",
            Code = "YT",
            Description = "Tài liệu lĩnh vực y tế và chăm sóc sức khỏe",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentField
        {
            Id = 3,
            Name = "Giáo dục",
            Code = "GD",
            Description = "Tài liệu lĩnh vực giáo dục và đào tạo",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentField
        {
            Id = 4,
            Name = "Kinh tế",
            Code = "KT",
            Description = "Tài liệu lĩnh vực kinh tế và tài chính",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentField
        {
            Id = 5,
            Name = "Công nghệ",
            Code = "CN",
            Description = "Tài liệu lĩnh vực công nghệ và kỹ thuật",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentField
        {
            Id = 6,
            Name = "Nông nghiệp",
            Code = "NN",
            Description = "Tài liệu lĩnh vực nông nghiệp và phát triển nông thôn",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentField
        {
            Id = 7,
            Name = "Môi trường",
            Code = "MT",
            Description = "Tài liệu lĩnh vực môi trường và phát triển bền vững",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentField
        {
            Id = 8,
            Name = "Văn hóa",
            Code = "VH",
            Description = "Tài liệu lĩnh vực văn hóa và nghệ thuật",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentField
        {
            Id = 9,
            Name = "Giao thông",
            Code = "GT",
            Description = "Tài liệu lĩnh vực giao thông và vận tải",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentField
        {
            Id = 10,
            Name = "Du lịch",
            Code = "DL",
            Description = "Tài liệu lĩnh vực du lịch và dịch vụ",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        }

    };
}
