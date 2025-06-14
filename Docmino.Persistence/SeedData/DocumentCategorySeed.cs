using Docmino.Domain.Entities;

namespace Docmino.Persistence.SeedData;

public class DocumentCategorySeed
{
    private static readonly DateTime defaultTime = new(2025, 01, 01);
    public static IEnumerable<DocumentCategory> DocumentCategories => new List<DocumentCategory>()
    {
        new DocumentCategory
        {
            Id = 1,
            Name = "Quyết định (cá biệt)",
            Code = "QD",
            Description = "Tài liệu quyết định (cá biệt)",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 2,
            Name = "Chỉ thị",
            Code = "CT",
            Description = "Tài liệu chỉ thị",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 3,
            Name = "Quy định",
            Code = "QD",
            Description = "Tài liệu quy định",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 4,
            Name = "Thông báo",
            Code = "TB",
            Description = "Tài liệu thông báo",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 5,
            Name = "Thông cáo",
            Code = "TC",
            Description = "Tài liệu thông cáo",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 6,
            Name = "Hướng dẫn",
            Code = "HD",
            Description = "Tài liệu hướng dẫn",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 7,
            Name = "Chương trình",
            Code = "CT",
            Description = "Tài liệu chương trình",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 8,
            Name = "Kế hoạch",
            Code = "KH",
            Description = "Tài liệu kế hoạch",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 9,
            Name = "Phương án",
            Code = "PA",
            Description = "Tài liệu phương án",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 10,
            Name = "Đề án",
            Code = "DA",
            Description = "Tài liệu đề án",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 11,
            Name = "Đơn",
            Code = "DA",
            Description = "Tài liệu đơn",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 12,
            Name = "Báo cáo",
            Code = "BC",
            Description = "Tài liệu báo cáo",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 13,
            Name = "Tờ trình",
            Code = "TT",
            Description = "Tài liệu tờ trình",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 14,
            Name = "Công điện",
            Code = "CD",
            Description = "Tài liệu công điện",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 15,
            Name = "Bản ghi nhớ",
            Code = "BGN",
            Description = "Tài liệu bản ghi nhớ",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 16,
            Name = "Giấy mời",
            Code = "GM",
            Description = "Tài liệu giấy mời",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 17,
            Name = "Giấy giới thiệu",
            Code = "GGT",
            Description = "Tài liệu giấy giới thiệu",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 18,
            Name = "Phiếu gửi",
            Code = "PG",
            Description = "Tài liệu phiếu gửi",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 19,
            Name = "Phiếu chuyển",
            Code = "PC",
            Description = "Tài liệu phiếu chuyển",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 20,
            Name = "Phiếu báo",
            Code = "PB",
            Description = "Tài liệu phiếu báo",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 21,
            Name = "Nghị định",
            Code = "ND",
            Description = "Tài liệu nghị định",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 22,
            Name = "Luật",
            Code = "LT",
            Description = "Tài liệu luật",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 23,
            Name = "Thông tư",
            Code = "TT",
            Description = "Tài liệu thông tư",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 24,
            Name = "Quy chế",
            Code = "QC",
            Description = "Tài liệu quy chế",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 25,
            Name = "Điều lệ",
            Code = "DL",
            Description = "Tài liệu điều lệ",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new DocumentCategory
        {
            Id = 26,
            Name = "Hợp đồng",
            Code = "HD",
            Description = "Tài liệu hợp đồng",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        }
    };
}