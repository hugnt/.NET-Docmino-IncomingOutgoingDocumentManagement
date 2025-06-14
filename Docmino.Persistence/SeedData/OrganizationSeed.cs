using Docmino.Domain.Entities;

namespace Docmino.Persistence.SeedData;

public class OrganizationSeed
{
    private static readonly DateTime defaultTime = new(2025, 01, 01);
    public static IEnumerable<Organization> Organizations => new List<Organization>()
    {
        new Organization
    {
        Id = 1,
        Name = "Công ty TNHH ABC",
        PhoneNumber = "0123456789",
        Email = "contact@abc.com",
        ContactPersonName = "Nguyễn Văn A",
        Description = "Tổ chức chuyên về dịch vụ tài chính.",
        CreatedAt = defaultTime,
        UpdatedAt = defaultTime,
        CreatedBy = Guid.Empty,
        UpdatedBy = Guid.Empty,
        IsDeleted = false
    },
        new Organization
        {
            Id = 2,
            Name = "Bộ Tài chính",
            PhoneNumber = "02438234567",
            Email = "contact@taichinh.gov.vn",
            ContactPersonName = "Trần Thị Bích",
            Description = "Bộ quản lý tài chính và ngân sách nhà nước.",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new Organization
        {
            Id = 3,
            Name = "Tập đoàn Điện lực Việt Nam (EVN)",
            PhoneNumber = "02466554433",
            Email = "info@evn.com.vn",
            ContactPersonName = "Lê Văn Cường",
            Description = "Tập đoàn nhà nước về sản xuất và phân phối điện năng.",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new Organization
        {
            Id = 4,
            Name = "Bộ Giao thông Vận tải",
            PhoneNumber = "02439422345",
            Email = "contact@gtvt.gov.vn",
            ContactPersonName = "Phạm Thị Duyên",
            Description = "Bộ quản lý giao thông và vận tải trên toàn quốc.",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new Organization
        {
            Id = 5,
            Name = "Tổng công ty Hàng không Việt Nam (Vietnam Airlines)",
            PhoneNumber = "02438765678",
            Email = "support@vietnamairlines.com",
            ContactPersonName = "Nguyễn Quốc Hùng",
            Description = "Hãng hàng không quốc gia Việt Nam.",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new Organization
        {
            Id = 6,
            Name = "Bộ Y tế",
            PhoneNumber = "02462732200",
            Email = "contact@y te.gov.vn",
            ContactPersonName = "Hoàng Thị Lan",
            Description = "Bộ quản lý y tế và chăm sóc sức khỏe cộng đồng.",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new Organization
        {
            Id = 7,
            Name = "Tập đoàn Dầu khí Việt Nam (Petrovietnam)",
            PhoneNumber = "02438252526",
            Email = "info@petrovietnam.com.vn",
            ContactPersonName = "Đỗ Văn Khánh",
            Description = "Tập đoàn nhà nước về khai thác và chế biến dầu khí.",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new Organization
        {
            Id = 8,
            Name = "Bộ Giáo dục và Đào tạo",
            PhoneNumber = "02439321155",
            Email = "contact@giaoduc.gov.vn",
            ContactPersonName = "Lê Thị Mai",
            Description = "Bộ quản lý giáo dục và đào tạo trên toàn quốc.",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new Organization
        {
            Id = 9,
            Name = "Tổng công ty Bưu điện Việt Nam (VNPost)",
            PhoneNumber = "02437689999",
            Email = "support@vnpost.vn",
            ContactPersonName = "Trần Văn Nam",
            Description = "Tổng công ty nhà nước về dịch vụ bưu chính.",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        },
        new Organization
        {
            Id = 10,
            Name = "Bộ Nông nghiệp và Phát triển Nông thôn",
            PhoneNumber = "02438446837",
            Email = "contact@nongnghiep.gov.vn",
            ContactPersonName = "Nguyễn Thị Hồng",
            Description = "Bộ quản lý nông nghiệp và phát triển nông thôn.",
            CreatedAt = defaultTime,
            UpdatedAt = defaultTime,
            CreatedBy = Guid.Empty,
            UpdatedBy = Guid.Empty,
            IsDeleted = false
        }
    };
}
