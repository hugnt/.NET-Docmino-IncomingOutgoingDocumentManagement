using Docmino.Domain.Entities;

namespace Docmino.Persistence.SeedData;

public class UserSeed
{
    private static readonly DateTime _defaultTime = new(2025, 01, 01);
    public static IEnumerable<User> Users => new List<User>()
    {
        new User
        {
            Id = Guid.Parse("3f8b2a1e-5c4d-4e9f-a2b3-7c8d9e0f1a2b"),
            Fullname = "Admin",
            Username = "admin",
            RoleId = 1,
            PasswordHash = "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F",
            Email = "admin@docmino.com",
            PositionId = 1,
            ImageSignature = "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620976/signature03_t5o01k.png",
            DigitalCertificate = "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new User
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Fullname = "Nguyễn Thu Trang",
            Username = "user001",
            RoleId = 2,
            PasswordHash = "C40D0CF1F0815D27829F76BA3F7B0399A9FF5BD6C05252B7F500B6826419EE25-E41A6B82F54C202A240A483B224F15C3",
            Email = "thanhhungst314@gmail.com",
            PositionId = 2,
            ImageSignature = "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620976/signature03_t5o01k.png",
            DigitalCertificate = "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx",
            CreateIncomingDocumentRight = true,
            CreateOutgoingDocumentRight = true,
            CreateInternalDocumentRight = true,
            InitialConfirmProcessRight = true,
            ManageCategories = true,
            ProcessManagerRight = true,
            StoreDocumentRight = true,
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new User
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Fullname = "Nguyễn Thành Hưng",
            Username = "user002",
            RoleId = 2,
            PasswordHash = "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F",
            Email = "thanh.hung.st302@gmail.com",
            PositionId = 2,
            ProcessManagerRight = true,
            StoreDocumentRight = true,
            ImageSignature = "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620976/signature02_vv1beq.png",
            DigitalCertificate = "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new User
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Fullname = "Doãn Nhật Anh",
            Username = "user003",
            RoleId = 3,
            PasswordHash = "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F",
            Email = "user03@gmail.com",
            PositionId = 2,
            ImageSignature = "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620975/signature_01_jkvh5v.png",
            DigitalCertificate = "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new User
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Fullname = "Doãn Nhật Đức",
            Username = "user004",
            RoleId = 3,
            PasswordHash = "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F",
            Email = "user04@gmail.com",
            PositionId = 2,
            ImageSignature = "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620975/signature_01_jkvh5v.png",
            DigitalCertificate = "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new User
        {
            Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            Fullname = "Doãn Nhật Hiếu",
            Username = "user005",
            RoleId = 3,
            PasswordHash = "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F",
            Email = "user05@gmail.com",
            PositionId = 2,
             ImageSignature = "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620975/signature_01_jkvh5v.png",
             DigitalCertificate = "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new User
        {
            Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
            Fullname = "Vũ Trung Anh",
            Username = "user006",
            RoleId = 3,
            PasswordHash = "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F",
            Email = "user06@gmail.com",
            PositionId = 2,
             ImageSignature = "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620975/signature_01_jkvh5v.png",
             DigitalCertificate = "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
        new User
        {
            Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
            Fullname = "Lê Quốc Việt",
            Username = "user007",
            RoleId = 3,
            PasswordHash = "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F",
            Email = "user06@gmail.com",
            PositionId = 2,
             ImageSignature = "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620975/signature_01_jkvh5v.png",
             DigitalCertificate = "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx",
            CreatedAt = _defaultTime,
            UpdatedAt = _defaultTime
        },
    };
}
