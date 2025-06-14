using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Docmino.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initDBV6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id0 = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Department_Id0",
                        column: x => x.Id0,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDirectory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ParentDirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDirectory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDirectory_DocumentDirectory_ParentDirectoryId",
                        column: x => x.ParentDirectoryId,
                        principalTable: "DocumentDirectory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRegister",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterType = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRegister", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoragePeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearAmount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoragePeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemFeature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Param = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id0 = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    StoragePeriodId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storage_DocumentDirectory_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "DocumentDirectory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Storage_StoragePeriod_StoragePeriodId",
                        column: x => x.StoragePeriodId,
                        principalTable: "StoragePeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    WalletAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DigitalCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateIncomingDocumentRight = table.Column<bool>(type: "bit", nullable: false),
                    CreateOutgoingDocumentRight = table.Column<bool>(type: "bit", nullable: false),
                    CreateInternalDocumentRight = table.Column<bool>(type: "bit", nullable: false),
                    InitialConfirmProcessRight = table.Column<bool>(type: "bit", nullable: false),
                    ProcessManagerRight = table.Column<bool>(type: "bit", nullable: false),
                    StoreDocumentRight = table.Column<bool>(type: "bit", nullable: false),
                    ManageCategories = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    DocumentRegisterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    CodeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeNotation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageAmount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurePriority = table.Column<int>(type: "int", nullable: false),
                    UrgentPriority = table.Column<int>(type: "int", nullable: false),
                    ArrivalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToPlaces = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IssuedAmount = table.Column<int>(type: "int", nullable: false),
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Document_DocumentCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "DocumentCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Document_DocumentField_FieldId",
                        column: x => x.FieldId,
                        principalTable: "DocumentField",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Document_DocumentRegister_DocumentRegisterId",
                        column: x => x.DocumentRegisterId,
                        principalTable: "DocumentRegister",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Document_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Document_Storage_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserFeature",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFeature", x => new { x.UserId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_UserFeature_SystemFeature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "SystemFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFeature_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Group_UserId",
                        column: x => x.UserId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroup_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfirmProcess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlockchainEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStepNumber = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfirmProcess_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfirmProcess_User_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<double>(type: "float", nullable: false),
                    FileEncoding = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentFile_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    ReviewerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReviewerGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReviewerPositionId = table.Column<int>(type: "int", nullable: true),
                    ReviewerDepartmentId = table.Column<int>(type: "int", nullable: true),
                    ReviewerType = table.Column<int>(type: "int", nullable: false),
                    SignType = table.Column<int>(type: "int", nullable: false),
                    VetoRight = table.Column<bool>(type: "bit", nullable: false),
                    DateStart = table.Column<DateOnly>(type: "date", nullable: false),
                    DateEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    ResignDateEnd = table.Column<DateOnly>(type: "date", nullable: true),
                    ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessDetail_ConfirmProcess_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "ConfirmProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessDetail_Department_ReviewerDepartmentId",
                        column: x => x.ReviewerDepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessDetail_Group_ReviewerGroupId",
                        column: x => x.ReviewerGroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessDetail_Position_ReviewerPositionId",
                        column: x => x.ReviewerPositionId,
                        principalTable: "Position",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessDetail_User_ReviewerUserId",
                        column: x => x.ReviewerUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStepNumber = table.Column<int>(type: "int", nullable: false),
                    CurrentStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserReviewerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextStepNumber = table.Column<int>(type: "int", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TxHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessHistory_ConfirmProcess_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "ConfirmProcess",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessHistory_User_UserReviewerId",
                        column: x => x.UserReviewerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessSignDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PosX = table.Column<double>(type: "float", nullable: false),
                    PosY = table.Column<double>(type: "float", nullable: false),
                    SignZoneWidth = table.Column<double>(type: "float", nullable: false),
                    SignZoneHeight = table.Column<double>(type: "float", nullable: false),
                    SignPage = table.Column<int>(type: "int", nullable: false),
                    TranslateX = table.Column<double>(type: "float", nullable: false),
                    TranslateY = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessSignDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessSignDetail_DocumentFile_FileId",
                        column: x => x.FileId,
                        principalTable: "DocumentFile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessSignDetail_ProcessDetail_ProcessDetailsId",
                        column: x => x.ProcessDetailsId,
                        principalTable: "ProcessDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessSignHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessSignHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessSignHistory_DocumentFile_OriginalFileId",
                        column: x => x.OriginalFileId,
                        principalTable: "DocumentFile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessSignHistory_ProcessHistory_ProcessHistoryId",
                        column: x => x.ProcessHistoryId,
                        principalTable: "ProcessHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "Description", "Id0", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "LD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, 1, false, "Lãnh đạo", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, "KD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, 2, false, "Phòng Kinh Doanh", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, "KT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, 3, false, "Phòng Kế Toán", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, "NS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, 4, false, "Phòng Nhân sự", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, "HC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, 5, false, "Phòng Hành Chính", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, "IT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, 6, false, "Phòng IT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, "DA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, 7, false, "Phòng Quản Lý Dự Án", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, "MKT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, 8, false, "Phòng Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, "DT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, 9, false, "Phòng Đào Tạo", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, "KS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, 10, false, "Phòng Kiểm Soát Nội Bộ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "DocumentCategory",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "QD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu quyết định (cá biệt)", false, "Quyết định (cá biệt)", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, "CT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu chỉ thị", false, "Chỉ thị", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, "QD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu quy định", false, "Quy định", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, "TB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu thông báo", false, "Thông báo", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, "TC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu thông cáo", false, "Thông cáo", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, "HD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu hướng dẫn", false, "Hướng dẫn", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, "CT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu chương trình", false, "Chương trình", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, "KH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu kế hoạch", false, "Kế hoạch", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, "PA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu phương án", false, "Phương án", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, "DA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu đề án", false, "Đề án", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, "DA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu đơn", false, "Đơn", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, "BC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu báo cáo", false, "Báo cáo", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, "TT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu tờ trình", false, "Tờ trình", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, "CD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu công điện", false, "Công điện", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, "BGN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu bản ghi nhớ", false, "Bản ghi nhớ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, "GM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu giấy mời", false, "Giấy mời", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, "GGT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu giấy giới thiệu", false, "Giấy giới thiệu", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, "PG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu phiếu gửi", false, "Phiếu gửi", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 19, "PC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu phiếu chuyển", false, "Phiếu chuyển", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 20, "PB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu phiếu báo", false, "Phiếu báo", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 21, "ND", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu nghị định", false, "Nghị định", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 22, "LT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu luật", false, "Luật", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 23, "TT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu thông tư", false, "Thông tư", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 24, "QC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu quy chế", false, "Quy chế", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 25, "DL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu điều lệ", false, "Điều lệ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 26, "HD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu hợp đồng", false, "Hợp đồng", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "DocumentField",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "PL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu lĩnh vực pháp luật", false, "Pháp luật", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, "YT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu lĩnh vực y tế và chăm sóc sức khỏe", false, "Y tế", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, "GD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu lĩnh vực giáo dục và đào tạo", false, "Giáo dục", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, "KT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu lĩnh vực kinh tế và tài chính", false, "Kinh tế", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, "CN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu lĩnh vực công nghệ và kỹ thuật", false, "Công nghệ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, "NN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu lĩnh vực nông nghiệp và phát triển nông thôn", false, "Nông nghiệp", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, "MT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu lĩnh vực môi trường và phát triển bền vững", false, "Môi trường", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, "VH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu lĩnh vực văn hóa và nghệ thuật", false, "Văn hóa", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, "GT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu lĩnh vực giao thông và vận tải", false, "Giao thông", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, "DL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tài liệu lĩnh vực du lịch và dịch vụ", false, "Du lịch", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "DocumentRegister",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsDeleted", "Name", "RegisterType", "UpdatedAt", "UpdatedBy", "Year" },
                values: new object[,]
                {
                    { new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Sổ đăng ký văn bản đến năm 2025", true, false, "Sổ đến năm 2025", 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2025 },
                    { new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Sổ đăng ký văn bản đi năm 2025", true, false, "Sổ đi năm 2025", 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2025 },
                    { new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Sổ đăng ký văn bản nội bộ năm 2025", true, false, "Sổ nội bộ năm 2025", 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2025 },
                    { new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Sổ tổng hợp đăng ký văn bản năm 2025", true, false, "Sổ tổng hợp năm 2025", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2025 },
                    { new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Sổ đặc biệt cho văn bản đến (đã lưu trữ)", false, false, "Sổ đặc biệt năm 2025", 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2025 }
                });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1688), new Guid("00000000-0000-0000-0000-000000000001"), false, "Admin", new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1691), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1699), new Guid("00000000-0000-0000-0000-000000000002"), false, "Manager", new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1700), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1718), new Guid("00000000-0000-0000-0000-000000000003"), false, "HR", new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1719), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1723), new Guid("00000000-0000-0000-0000-000000000004"), false, "IT", new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1724), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1728), new Guid("00000000-0000-0000-0000-000000000005"), false, "Finance", new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1729), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1736), new Guid("00000000-0000-0000-0000-000000000006"), false, "Guest", new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1736), new Guid("00000000-0000-0000-0000-000000000006") }
                });

            migrationBuilder.InsertData(
                table: "Organization",
                columns: new[] { "Id", "ContactPersonName", "CreatedAt", "CreatedBy", "Description", "Email", "IsDeleted", "Name", "PhoneNumber", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "Nguyễn Văn A", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tổ chức chuyên về dịch vụ tài chính.", "contact@abc.com", false, "Công ty TNHH ABC", "0123456789", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, "Trần Thị Bích", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Bộ quản lý tài chính và ngân sách nhà nước.", "contact@taichinh.gov.vn", false, "Bộ Tài chính", "02438234567", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, "Lê Văn Cường", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tập đoàn nhà nước về sản xuất và phân phối điện năng.", "info@evn.com.vn", false, "Tập đoàn Điện lực Việt Nam (EVN)", "02466554433", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, "Phạm Thị Duyên", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Bộ quản lý giao thông và vận tải trên toàn quốc.", "contact@gtvt.gov.vn", false, "Bộ Giao thông Vận tải", "02439422345", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, "Nguyễn Quốc Hùng", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Hãng hàng không quốc gia Việt Nam.", "support@vietnamairlines.com", false, "Tổng công ty Hàng không Việt Nam (Vietnam Airlines)", "02438765678", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, "Hoàng Thị Lan", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Bộ quản lý y tế và chăm sóc sức khỏe cộng đồng.", "contact@y te.gov.vn", false, "Bộ Y tế", "02462732200", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, "Đỗ Văn Khánh", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tập đoàn nhà nước về khai thác và chế biến dầu khí.", "info@petrovietnam.com.vn", false, "Tập đoàn Dầu khí Việt Nam (Petrovietnam)", "02438252526", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, "Lê Thị Mai", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Bộ quản lý giáo dục và đào tạo trên toàn quốc.", "contact@giaoduc.gov.vn", false, "Bộ Giáo dục và Đào tạo", "02439321155", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, "Trần Văn Nam", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Tổng công ty nhà nước về dịch vụ bưu chính.", "support@vnpost.vn", false, "Tổng công ty Bưu điện Việt Nam (VNPost)", "02437689999", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, "Nguyễn Thị Hồng", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Bộ quản lý nông nghiệp và phát triển nông thôn.", "contact@nongnghiep.gov.vn", false, "Bộ Nông nghiệp và Phát triển Nông thôn", "02438446837", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "ADMIN", "Admin" },
                    { 2, "CLERICAL_ASSISTANT", "Chuyên viên văn thư" },
                    { 3, "APPROVER", "Người kí & duyệt" }
                });

            migrationBuilder.InsertData(
                table: "StoragePeriod",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy", "YearAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Storage period for 1 year.", false, "Ngắn hạn (1 năm)", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1 },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Storage period for 5 years.", false, "5 năm", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 5 },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Storage period for 10 years.", false, "10 năm", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 10 },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Storage period with no expiration (forever).", false, "Vĩnh viễn", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2147483647 }
                });

            migrationBuilder.InsertData(
                table: "Document",
                columns: new[] { "Id", "ArrivalDate", "ArrivalNumber", "CategoryId", "CodeNotation", "CodeNumber", "CreatedAt", "CreatedBy", "DepartmentId", "Description", "DocumentRegisterId", "DocumentStatus", "DueDate", "FieldId", "IsDeleted", "IssuedAmount", "IssuedDate", "Name", "OrganizationId", "PageAmount", "SecurePriority", "StorageId", "Subject", "ToPlaces", "UpdatedAt", "UpdatedBy", "UrgentPriority" },
                values: new object[,]
                {
                    { new Guid("044abecc-a98a-4f2b-b2d5-ac5ce71b3fad"), new DateOnly(2025, 1, 2), "ĐN-28", 1, "A-2025-6", "VB-28", new DateTime(2025, 4, 9, 4, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Thông báo thay đổi quy chế", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Thông báo thay đổi quy chế", 1, 5, 0, null, "Nội dung: Thông báo thay đổi quy chế", "Phòng ban 28", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("04de90fc-7be6-4e2a-88c1-1fbeca6b2278"), new DateOnly(2025, 1, 2), "ĐN-26", 1, "A-2025-6", "VB-26", new DateTime(2025, 4, 29, 11, 19, 47, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Biên bản bàn giao tài sản", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Biên bản bàn giao tài sản", 1, 5, 0, null, "Nội dung: Biên bản bàn giao tài sản", "Phòng ban 26", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("08ee2081-8d42-4c10-bdfe-5258abfd5c16"), new DateOnly(2025, 1, 2), "ĐN-43", 1, "A-2025-8", "VB-43", new DateTime(2025, 2, 22, 6, 50, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Thông báo tuyển dụng nhân sự", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Thông báo tuyển dụng nhân sự", 1, 5, 0, null, "Nội dung: Thông báo tuyển dụng nhân sự", "Phòng ban 43", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("0b1ad12d-4f74-46f1-a5ea-dd197ace0ad0"), new DateOnly(2025, 1, 2), "ĐN-10", 1, "A-2025-2", "VB-10", new DateTime(2025, 2, 17, 17, 41, 37, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo tài chính quý I năm 2025", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo tài chính quý I năm 2025", 1, 5, 0, null, "Nội dung: Báo cáo tài chính quý I năm 2025", "Phòng ban 10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateOnly(2025, 1, 2), "ĐN-6", 1, "A-2025-2", "VB-6", new DateTime(2025, 4, 21, 15, 30, 50, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Quyết định bổ nhiệm cán bộ", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Quyết định bổ nhiệm cán bộ", 1, 5, 0, null, "Nội dung: Quyết định bổ nhiệm cán bộ", "Phòng ban 6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("173e8831-af4d-4b94-b038-96f1a4dcf6b0"), new DateOnly(2025, 1, 2), "ĐN-35", 1, "A-2025-6", "VB-35", new DateTime(2025, 1, 12, 12, 55, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Công văn chỉ đạo phòng chống dịch", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Công văn chỉ đạo phòng chống dịch", 1, 5, 0, null, "Nội dung: Công văn chỉ đạo phòng chống dịch", "Phòng ban 35", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("1f7f51b9-dcd1-4628-9c6e-034be3384d83"), new DateOnly(2025, 1, 2), "ĐN-66", 1, "A-2025-12", "VB-66", new DateTime(2025, 2, 7, 9, 17, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo đánh giá hiệu quả công việc", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo đánh giá hiệu quả công việc", 1, 5, 0, null, "Nội dung: Báo cáo đánh giá hiệu quả công việc", "Phòng ban 66", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateOnly(2025, 1, 2), "ĐN-7", 1, "A-2025-2", "VB-7", new DateTime(2025, 1, 10, 0, 5, 37, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Tờ trình xin ý kiến chỉ đạo", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Tờ trình xin ý kiến chỉ đạo", 1, 5, 0, null, "Nội dung: Tờ trình xin ý kiến chỉ đạo", "Phòng ban 7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("24eabcc2-8e22-49d4-aa45-af757dca56a4"), new DateOnly(2025, 1, 2), "ĐN-18", 1, "A-2025-4", "VB-18", new DateTime(2025, 3, 7, 5, 9, 35, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Công văn gửi các đơn vị trực thuộc", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Công văn gửi các đơn vị trực thuộc", 1, 5, 0, null, "Nội dung: Công văn gửi các đơn vị trực thuộc", "Phòng ban 18", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("2881d90e-b622-45ef-ab1b-aa7ef911fd0f"), new DateOnly(2025, 1, 2), "ĐN-54", 1, "A-2025-10", "VB-54", new DateTime(2025, 4, 4, 14, 47, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Giấy triệu tập họp khẩn", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Giấy triệu tập họp khẩn", 1, 5, 0, null, "Nội dung: Giấy triệu tập họp khẩn", "Phòng ban 54", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("2a47d7f9-17f7-4fef-aa4e-413f2356e249"), new DateOnly(2025, 1, 2), "ĐN-15", 1, "A-2025-3", "VB-15", new DateTime(2025, 1, 7, 21, 18, 54, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Biên bản nghiệm thu dự án", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Biên bản nghiệm thu dự án", 1, 5, 0, null, "Nội dung: Biên bản nghiệm thu dự án", "Phòng ban 15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("2c4d4d72-4fad-4854-a029-de72684d2367"), new DateOnly(2025, 1, 2), "ĐN-65", 1, "A-2025-11", "VB-65", new DateTime(2025, 5, 30, 16, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Thông báo nghỉ lễ Tết Nguyên Đán", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Thông báo nghỉ lễ Tết Nguyên Đán", 1, 5, 0, null, "Nội dung: Thông báo nghỉ lễ Tết Nguyên Đán", "Phòng ban 65", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("30c6bd44-e3c7-4e8b-9d53-37769b28618f"), new DateOnly(2025, 1, 2), "ĐN-40", 1, "A-2025-7", "VB-40", new DateTime(2025, 5, 15, 2, 55, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Công văn yêu cầu báo cáo tiến độ", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Công văn yêu cầu báo cáo tiến độ", 1, 5, 0, null, "Nội dung: Công văn yêu cầu báo cáo tiến độ", "Phòng ban 40", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("30f64f16-f6de-468f-9834-cf0f827ec470"), new DateOnly(2025, 1, 2), "ĐN-70", 1, "A-2025-12", "VB-70", new DateTime(2025, 2, 12, 7, 44, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch cải tiến quy trình làm việc", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch cải tiến quy trình làm việc", 1, 5, 0, null, "Nội dung: Kế hoạch cải tiến quy trình làm việc", "Phòng ban 70", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateOnly(2025, 1, 2), "ĐN-9", 1, "A-2025-2", "VB-9", new DateTime(2025, 2, 2, 10, 54, 46, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Công văn yêu cầu báo cáo tiến độ", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Công văn yêu cầu báo cáo tiến độ", 1, 5, 0, null, "Nội dung: Công văn yêu cầu báo cáo tiến độ", "Phòng ban 9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("41e40cf4-bcb9-44b0-92b7-df6626472175"), new DateOnly(2025, 1, 2), "ĐN-30", 1, "A-2025-6", "VB-30", new DateTime(2025, 3, 26, 0, 10, 1, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Tờ trình xin phê duyệt dự án", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Tờ trình xin phê duyệt dự án", 1, 5, 0, null, "Nội dung: Tờ trình xin phê duyệt dự án", "Phòng ban 30", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("495f3353-c036-41cb-8157-a2055282cce3"), new DateOnly(2025, 1, 2), "ĐN-50", 1, "A-2025-9", "VB-50", new DateTime(2025, 5, 24, 22, 3, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Tờ trình xin ý kiến chỉ đạo", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Tờ trình xin ý kiến chỉ đạo", 1, 5, 0, null, "Nội dung: Tờ trình xin ý kiến chỉ đạo", "Phòng ban 50", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("5072b563-a1bb-4535-86ac-7b7821b33896"), new DateOnly(2025, 1, 2), "ĐN-21", 1, "A-2025-5", "VB-21", new DateTime(2025, 1, 3, 22, 55, 24, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch tổ chức sự kiện", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch tổ chức sự kiện", 1, 5, 0, null, "Nội dung: Kế hoạch tổ chức sự kiện", "Phòng ban 21", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("524014f5-91a4-4d2d-90ff-a9478795479b"), new DateOnly(2025, 1, 2), "ĐN-25", 1, "A-2025-5", "VB-25", new DateTime(2025, 2, 21, 0, 12, 40, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Công văn góp ý dự thảo văn bản", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Công văn góp ý dự thảo văn bản", 1, 5, 0, null, "Nội dung: Công văn góp ý dự thảo văn bản", "Phòng ban 25", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("547a2ecf-1e4a-46bd-a2b4-01694abaefcf"), new DateOnly(2025, 1, 2), "ĐN-73", 1, "A-2025-13", "VB-73", new DateTime(2025, 2, 16, 3, 58, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Tờ trình xin phê duyệt dự án", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Tờ trình xin phê duyệt dự án", 1, 5, 0, null, "Nội dung: Tờ trình xin phê duyệt dự án", "Phòng ban 73", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("556ff713-ce14-4596-bb6a-3da0e581b006"), new DateOnly(2025, 1, 2), "ĐN-13", 1, "A-2025-3", "VB-13", new DateTime(2025, 6, 5, 1, 45, 24, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo tình hình thực hiện kế hoạch", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo tình hình thực hiện kế hoạch", 1, 5, 0, null, "Nội dung: Báo cáo tình hình thực hiện kế hoạch", "Phòng ban 13", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("5953b23f-67f2-42e1-be51-c87e95ea13da"), new DateOnly(2025, 1, 2), "ĐN-38", 1, "A-2025-7", "VB-38", new DateTime(2025, 2, 27, 8, 59, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch tổ chức sự kiện", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch tổ chức sự kiện", 1, 5, 0, null, "Nội dung: Kế hoạch tổ chức sự kiện", "Phòng ban 38", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("5ee4f172-1d73-42fb-8c74-8047c18635b7"), new DateOnly(2025, 1, 2), "ĐN-20", 1, "A-2025-4", "VB-20", new DateTime(2025, 1, 19, 21, 24, 16, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Tờ trình đề nghị hỗ trợ kinh phí", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Tờ trình đề nghị hỗ trợ kinh phí", 1, 5, 0, null, "Nội dung: Tờ trình đề nghị hỗ trợ kinh phí", "Phòng ban 20", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("5fe83d71-c820-44bb-940f-4a8dab57f584"), new DateOnly(2025, 1, 2), "ĐN-61", 1, "A-2025-11", "VB-61", new DateTime(2025, 6, 3, 18, 37, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Biên bản bàn giao tài sản", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Biên bản bàn giao tài sản", 1, 5, 0, null, "Nội dung: Biên bản bàn giao tài sản", "Phòng ban 61", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("624ccf16-1373-4b10-805b-61a8443d9b6d"), new DateOnly(2025, 1, 2), "ĐN-60", 1, "A-2025-11", "VB-60", new DateTime(2025, 4, 20, 23, 57, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Văn bản hướng dẫn thực hiện chính sách", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Văn bản hướng dẫn thực hiện chính sách", 1, 5, 0, null, "Nội dung: Văn bản hướng dẫn thực hiện chính sách", "Phòng ban 60", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("6342ed23-36ec-455a-ab3e-65c8c657111b"), new DateOnly(2025, 1, 2), "ĐN-53", 1, "A-2025-9", "VB-53", new DateTime(2025, 1, 20, 6, 49, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo đánh giá hiệu quả công việc", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo đánh giá hiệu quả công việc", 1, 5, 0, null, "Nội dung: Báo cáo đánh giá hiệu quả công việc", "Phòng ban 53", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("638cd3f6-8172-4127-846d-ff3a9dc6489d"), new DateOnly(2025, 1, 2), "ĐN-55", 1, "A-2025-10", "VB-55", new DateTime(2025, 4, 11, 3, 39, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Tờ trình xin phê duyệt dự án", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Tờ trình xin phê duyệt dự án", 1, 5, 0, null, "Nội dung: Tờ trình xin phê duyệt dự án", "Phòng ban 55", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("645cf45a-d04f-4ab9-974d-a316fe528883"), new DateOnly(2025, 1, 2), "ĐN-49", 1, "A-2025-9", "VB-49", new DateTime(2025, 4, 7, 6, 16, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Quyết định xử lý kỷ luật", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Quyết định xử lý kỷ luật", 1, 5, 0, null, "Nội dung: Quyết định xử lý kỷ luật", "Phòng ban 49", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("65eb1e36-6977-4431-84d1-d981dbb29cc2"), new DateOnly(2025, 1, 2), "ĐN-56", 1, "A-2025-10", "VB-56", new DateTime(2025, 3, 2, 0, 49, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Tờ trình đề nghị hỗ trợ kinh phí", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Tờ trình đề nghị hỗ trợ kinh phí", 1, 5, 0, null, "Nội dung: Tờ trình đề nghị hỗ trợ kinh phí", "Phòng ban 56", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("681dbce4-31c7-4a76-810f-f9762bcf34b8"), new DateOnly(2025, 1, 2), "ĐN-57", 1, "A-2025-10", "VB-57", new DateTime(2025, 2, 9, 6, 29, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo tổng kết công tác năm 2024", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo tổng kết công tác năm 2024", 1, 5, 0, null, "Nội dung: Báo cáo tổng kết công tác năm 2024", "Phòng ban 57", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("7b11d983-3557-423b-bfcc-02ca826be1af"), new DateOnly(2025, 1, 2), "ĐN-44", 1, "A-2025-8", "VB-44", new DateTime(2025, 3, 22, 19, 8, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch cải tiến quy trình làm việc", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch cải tiến quy trình làm việc", 1, 5, 0, null, "Nội dung: Kế hoạch cải tiến quy trình làm việc", "Phòng ban 44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("7bfdbf26-22a2-4660-8f02-0a27b86a07c5"), new DateOnly(2025, 1, 2), "ĐN-78", 1, "A-2025-14", "VB-78", new DateTime(2025, 4, 10, 19, 54, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Giấy triệu tập họp khẩn", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Giấy triệu tập họp khẩn", 1, 5, 0, null, "Nội dung: Giấy triệu tập họp khẩn", "Phòng ban 78", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("821770a9-31f3-4847-99d7-cbf36797dcd8"), new DateOnly(2025, 1, 2), "ĐN-76", 1, "A-2025-13", "VB-76", new DateTime(2025, 6, 1, 16, 3, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Văn bản phân công nhiệm vụ", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Văn bản phân công nhiệm vụ", 1, 5, 0, null, "Nội dung: Văn bản phân công nhiệm vụ", "Phòng ban 76", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("8624ae93-52ed-4f9d-ac3c-394f2bdf4e44"), new DateOnly(2025, 1, 2), "ĐN-51", 1, "A-2025-9", "VB-51", new DateTime(2025, 1, 24, 12, 19, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Biên bản họp đánh giá nhân sự", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Biên bản họp đánh giá nhân sự", 1, 5, 0, null, "Nội dung: Biên bản họp đánh giá nhân sự", "Phòng ban 51", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("864dde4e-d834-4e44-bdc0-ad1bef8def4c"), new DateOnly(2025, 1, 2), "ĐN-48", 1, "A-2025-9", "VB-48", new DateTime(2025, 1, 26, 0, 31, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Quyết định bổ nhiệm cán bộ", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Quyết định bổ nhiệm cán bộ", 1, 5, 0, null, "Nội dung: Quyết định bổ nhiệm cán bộ", "Phòng ban 48", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("872d6dcc-629a-44c4-b678-fd8ba90d4690"), new DateOnly(2025, 1, 2), "ĐN-46", 1, "A-2025-8", "VB-46", new DateTime(2025, 4, 9, 14, 20, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Công văn góp ý dự thảo văn bản", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Công văn góp ý dự thảo văn bản", 1, 5, 0, null, "Nội dung: Công văn góp ý dự thảo văn bản", "Phòng ban 46", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("8819ee05-3f0e-4bf0-8fcf-d1669568791c"), new DateOnly(2025, 1, 2), "ĐN-37", 1, "A-2025-7", "VB-37", new DateTime(2025, 1, 14, 8, 11, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Công văn chỉ đạo phòng chống dịch", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Công văn chỉ đạo phòng chống dịch", 1, 5, 0, null, "Nội dung: Công văn chỉ đạo phòng chống dịch", "Phòng ban 37", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("888c2ff8-c791-44b7-a117-f3d457520f10"), new DateOnly(2025, 1, 2), "ĐN-17", 1, "A-2025-4", "VB-17", new DateTime(2025, 3, 1, 11, 39, 11, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Văn bản đề xuất mua sắm thiết bị", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Văn bản đề xuất mua sắm thiết bị", 1, 5, 0, null, "Nội dung: Văn bản đề xuất mua sắm thiết bị", "Phòng ban 17", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("8a556d4a-0609-4d58-bc4a-8789faf1667b"), new DateOnly(2025, 1, 2), "ĐN-29", 1, "A-2025-6", "VB-29", new DateTime(2025, 4, 9, 6, 38, 15, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch cải tiến quy trình làm việc", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch cải tiến quy trình làm việc", 1, 5, 0, null, "Nội dung: Kế hoạch cải tiến quy trình làm việc", "Phòng ban 29", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("8ad9213b-2178-488d-aa58-0fe6cd14ba7a"), new DateOnly(2025, 1, 2), "ĐN-36", 1, "A-2025-7", "VB-36", new DateTime(2025, 3, 27, 6, 5, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Công văn gửi các đơn vị trực thuộc", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Công văn gửi các đơn vị trực thuộc", 1, 5, 0, null, "Nội dung: Công văn gửi các đơn vị trực thuộc", "Phòng ban 36", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("96703c13-11b4-4a1b-aaab-515026c1f2e1"), new DateOnly(2025, 1, 2), "ĐN-72", 1, "A-2025-13", "VB-72", new DateTime(2025, 1, 17, 12, 9, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch đào tạo nội bộ", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch đào tạo nội bộ", 1, 5, 0, null, "Nội dung: Kế hoạch đào tạo nội bộ", "Phòng ban 72", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("98047b4a-84ad-45fd-b608-497e3d98220a"), new DateOnly(2025, 1, 2), "ĐN-45", 1, "A-2025-8", "VB-45", new DateTime(2025, 1, 27, 18, 50, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch đào tạo nội bộ", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch đào tạo nội bộ", 1, 5, 0, null, "Nội dung: Kế hoạch đào tạo nội bộ", "Phòng ban 45", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("98ca313b-d56c-4599-bba3-460d8d42d4e9"), new DateOnly(2025, 1, 2), "ĐN-75", 1, "A-2025-13", "VB-75", new DateTime(2025, 5, 10, 16, 51, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Biên bản bàn giao tài sản", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Biên bản bàn giao tài sản", 1, 5, 0, null, "Nội dung: Biên bản bàn giao tài sản", "Phòng ban 75", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("9a11c885-d321-4107-ac68-a4c2b5596c30"), new DateOnly(2025, 1, 2), "ĐN-52", 1, "A-2025-9", "VB-52", new DateTime(2025, 1, 12, 16, 52, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch triển khai dự án A", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch triển khai dự án A", 1, 5, 0, null, "Nội dung: Kế hoạch triển khai dự án A", "Phòng ban 52", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("9d2ddcc6-79f9-46f8-9efe-96995737c3c2"), new DateOnly(2025, 1, 2), "ĐN-34", 1, "A-2025-6", "VB-34", new DateTime(2025, 3, 20, 7, 2, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch đào tạo nội bộ", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch đào tạo nội bộ", 1, 5, 0, null, "Nội dung: Kế hoạch đào tạo nội bộ", "Phòng ban 34", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("a08d8d68-2691-4c15-8998-ad932cdda0f7"), new DateOnly(2025, 1, 2), "ĐN-32", 1, "A-2025-6", "VB-32", new DateTime(2025, 5, 21, 2, 15, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo tài chính quý I năm 2025", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo tài chính quý I năm 2025", 1, 5, 0, null, "Nội dung: Báo cáo tài chính quý I năm 2025", "Phòng ban 32", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("a4939516-29d3-4780-a767-df2256c6d5a0"), new DateOnly(2025, 1, 2), "ĐN-24", 1, "A-2025-5", "VB-24", new DateTime(2025, 2, 12, 20, 58, 6, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo đánh giá hiệu quả công việc", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo đánh giá hiệu quả công việc", 1, 5, 0, null, "Nội dung: Báo cáo đánh giá hiệu quả công việc", "Phòng ban 24", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("a6a1e2b0-5437-4055-85fa-6b5e66c70854"), new DateOnly(2025, 1, 2), "ĐN-22", 1, "A-2025-5", "VB-22", new DateTime(2025, 1, 1, 22, 49, 45, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Thông báo thay đổi lịch làm việc", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Thông báo thay đổi lịch làm việc", 1, 5, 0, null, "Nội dung: Thông báo thay đổi lịch làm việc", "Phòng ban 22", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("a7c0311a-f209-422e-ac53-5d4ab8215d27"), new DateOnly(2025, 1, 2), "ĐN-62", 1, "A-2025-11", "VB-62", new DateTime(2025, 5, 20, 14, 31, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo tình hình thực hiện kế hoạch", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo tình hình thực hiện kế hoạch", 1, 5, 0, null, "Nội dung: Báo cáo tình hình thực hiện kế hoạch", "Phòng ban 62", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("a9b042c9-239c-4cf9-8201-d324697956af"), new DateOnly(2025, 1, 2), "ĐN-14", 1, "A-2025-3", "VB-14", new DateTime(2025, 1, 18, 8, 46, 27, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Giấy mời họp giao ban tháng", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Giấy mời họp giao ban tháng", 1, 5, 0, null, "Nội dung: Giấy mời họp giao ban tháng", "Phòng ban 14", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateOnly(2025, 1, 2), "ĐN-1", 1, "A-2025-1", "VB-1", new DateTime(2025, 1, 5, 9, 53, 46, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo tổng kết công tác năm 2024", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo tổng kết công tác năm 2024", 1, 5, 0, null, "Nội dung: Báo cáo tổng kết công tác năm 2024", "Phòng ban 1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("ab9d21c6-210b-4b30-9505-ba75fdc49e95"), new DateOnly(2025, 1, 2), "ĐN-41", 1, "A-2025-7", "VB-41", new DateTime(2025, 3, 7, 23, 28, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Văn bản hướng dẫn thực hiện chính sách", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Văn bản hướng dẫn thực hiện chính sách", 1, 5, 0, null, "Nội dung: Văn bản hướng dẫn thực hiện chính sách", "Phòng ban 41", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("b14837de-cb87-46fc-b32b-d62c38d01380"), new DateOnly(2025, 1, 2), "ĐN-27", 1, "A-2025-6", "VB-27", new DateTime(2025, 3, 9, 16, 18, 52, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Giấy triệu tập họp khẩn", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Giấy triệu tập họp khẩn", 1, 5, 0, null, "Nội dung: Giấy triệu tập họp khẩn", "Phòng ban 27", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("ba0e46db-9a65-4124-b830-12d326bc1bad"), new DateOnly(2025, 1, 2), "ĐN-69", 1, "A-2025-12", "VB-69", new DateTime(2025, 4, 9, 12, 49, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Quyết định bổ nhiệm cán bộ", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Quyết định bổ nhiệm cán bộ", 1, 5, 0, null, "Nội dung: Quyết định bổ nhiệm cán bộ", "Phòng ban 69", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("ba4db2b7-c5d4-494b-b166-7ee2e1f9a763"), new DateOnly(2025, 1, 2), "ĐN-68", 1, "A-2025-12", "VB-68", new DateTime(2025, 5, 13, 14, 55, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Văn bản phân công nhiệm vụ", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Văn bản phân công nhiệm vụ", 1, 5, 0, null, "Nội dung: Văn bản phân công nhiệm vụ", "Phòng ban 68", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("bb2747ff-9869-4348-87b3-2be85d56da22"), new DateOnly(2025, 1, 2), "ĐN-39", 1, "A-2025-7", "VB-39", new DateTime(2025, 1, 20, 13, 31, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Quyết định xử lý kỷ luật", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Quyết định xử lý kỷ luật", 1, 5, 0, null, "Nội dung: Quyết định xử lý kỷ luật", "Phòng ban 39", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateOnly(2025, 1, 2), "ĐN-4", 1, "A-2025-1", "VB-4", new DateTime(2025, 4, 20, 18, 19, 44, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Công văn chỉ đạo phòng chống dịch", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Công văn chỉ đạo phòng chống dịch", 1, 5, 0, null, "Nội dung: Công văn chỉ đạo phòng chống dịch", "Phòng ban 4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("c022676e-d91b-405b-9add-491dfadf6a01"), new DateOnly(2025, 1, 2), "ĐN-16", 1, "A-2025-4", "VB-16", new DateTime(2025, 5, 9, 6, 33, 52, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Thông báo thay đổi nhân sự", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Thông báo thay đổi nhân sự", 1, 5, 0, null, "Nội dung: Thông báo thay đổi nhân sự", "Phòng ban 16", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("cb9cfbb5-9450-48ce-946c-888d15f54247"), new DateOnly(2025, 1, 2), "ĐN-63", 1, "A-2025-11", "VB-63", new DateTime(2025, 5, 28, 8, 36, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Biên bản họp đánh giá nhân sự", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Biên bản họp đánh giá nhân sự", 1, 5, 0, null, "Nội dung: Biên bản họp đánh giá nhân sự", "Phòng ban 63", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("ccc3a957-7955-4619-9c80-0c813318e29a"), new DateOnly(2025, 1, 2), "ĐN-58", 1, "A-2025-10", "VB-58", new DateTime(2025, 2, 4, 6, 35, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Thông báo thay đổi nhân sự", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Thông báo thay đổi nhân sự", 1, 5, 0, null, "Nội dung: Thông báo thay đổi nhân sự", "Phòng ban 58", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateOnly(2025, 1, 2), "ĐN-3", 1, "A-2025-1", "VB-3", new DateTime(2025, 4, 24, 10, 51, 26, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Thông báo nghỉ lễ Tết Nguyên Đán", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Thông báo nghỉ lễ Tết Nguyên Đán", 1, 5, 0, null, "Nội dung: Thông báo nghỉ lễ Tết Nguyên Đán", "Phòng ban 3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("ce47f3d3-17d7-4b85-b11b-434ed1968fb8"), new DateOnly(2025, 1, 2), "ĐN-23", 1, "A-2025-5", "VB-23", new DateTime(2025, 4, 28, 3, 40, 19, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Văn bản phân công nhiệm vụ", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Văn bản phân công nhiệm vụ", 1, 5, 0, null, "Nội dung: Văn bản phân công nhiệm vụ", "Phòng ban 23", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("ce541526-60cb-4861-a6de-6deb9f68395e"), new DateOnly(2025, 1, 2), "ĐN-59", 1, "A-2025-10", "VB-59", new DateTime(2025, 4, 1, 10, 6, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch cải tiến quy trình làm việc", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch cải tiến quy trình làm việc", 1, 5, 0, null, "Nội dung: Kế hoạch cải tiến quy trình làm việc", "Phòng ban 59", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("cfa1f5e8-c045-4dea-9433-53c4243763e4"), new DateOnly(2025, 1, 2), "ĐN-31", 1, "A-2025-6", "VB-31", new DateTime(2025, 1, 4, 2, 24, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo tài chính quý I năm 2025", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo tài chính quý I năm 2025", 1, 5, 0, null, "Nội dung: Báo cáo tài chính quý I năm 2025", "Phòng ban 31", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("dc8fcfdf-1e19-4555-861e-3cc9ca704874"), new DateOnly(2025, 1, 2), "ĐN-11", 1, "A-2025-3", "VB-11", new DateTime(2025, 4, 28, 19, 42, 52, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch đào tạo nội bộ", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch đào tạo nội bộ", 1, 5, 0, null, "Nội dung: Kế hoạch đào tạo nội bộ", "Phòng ban 11", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateOnly(2025, 1, 2), "ĐN-2", 1, "A-2025-1", "VB-2", new DateTime(2025, 4, 20, 2, 13, 9, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Kế hoạch triển khai dự án A", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Kế hoạch triển khai dự án A", 1, 5, 0, null, "Nội dung: Kế hoạch triển khai dự án A", "Phòng ban 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("dfe3223c-48a4-4076-96a8-c3439ac9ae09"), new DateOnly(2025, 1, 2), "ĐN-71", 1, "A-2025-12", "VB-71", new DateTime(2025, 4, 14, 8, 26, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Thông báo tuyển dụng nhân sự", new Guid("b1a1c1d1-e1f1-41a1-b1c1-d1e1f1a1b1c1"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Thông báo tuyển dụng nhân sự", 1, 5, 0, null, "Nội dung: Thông báo tuyển dụng nhân sự", "Phòng ban 71", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("e0412428-d158-48bb-af2b-dbedbc8eced6"), new DateOnly(2025, 1, 2), "ĐN-12", 1, "A-2025-3", "VB-12", new DateTime(2025, 2, 28, 16, 2, 40, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Văn bản hướng dẫn thực hiện chính sách", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Văn bản hướng dẫn thực hiện chính sách", 1, 5, 0, null, "Nội dung: Văn bản hướng dẫn thực hiện chính sách", "Phòng ban 12", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("e45902d9-7b93-4a64-9633-33039ade86d0"), new DateOnly(2025, 1, 2), "ĐN-80", 1, "A-2025-14", "VB-80", new DateTime(2025, 3, 31, 16, 26, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Tờ trình đề nghị hỗ trợ kinh phí", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Tờ trình đề nghị hỗ trợ kinh phí", 1, 5, 0, null, "Nội dung: Tờ trình đề nghị hỗ trợ kinh phí", "Phòng ban 80", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("e7670a6a-a133-4ae8-bb0d-10592c0817aa"), new DateOnly(2025, 1, 2), "ĐN-77", 1, "A-2025-13", "VB-77", new DateTime(2025, 2, 20, 19, 34, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo tài chính quý I năm 2025", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo tài chính quý I năm 2025", 1, 5, 0, null, "Nội dung: Báo cáo tài chính quý I năm 2025", "Phòng ban 77", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("eadab4eb-a7fa-479a-b598-2066cfb39034"), new DateOnly(2025, 1, 2), "ĐN-33", 1, "A-2025-6", "VB-33", new DateTime(2025, 1, 30, 16, 29, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Quyết định xử lý kỷ luật", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Quyết định xử lý kỷ luật", 1, 5, 0, null, "Nội dung: Quyết định xử lý kỷ luật", "Phòng ban 33", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("eb95b546-bf5d-47b2-805d-1d5eac93f4e5"), new DateOnly(2025, 1, 2), "ĐN-79", 1, "A-2025-14", "VB-79", new DateTime(2025, 1, 3, 22, 26, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Biên bản họp đánh giá nhân sự", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Biên bản họp đánh giá nhân sự", 1, 5, 0, null, "Nội dung: Biên bản họp đánh giá nhân sự", "Phòng ban 79", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("eddf0044-6949-4104-ac4a-f50dd3e168d1"), new DateOnly(2025, 1, 2), "ĐN-64", 1, "A-2025-11", "VB-64", new DateTime(2025, 3, 31, 8, 49, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Biên bản nghiệm thu dự án", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Biên bản nghiệm thu dự án", 1, 5, 0, null, "Nội dung: Biên bản nghiệm thu dự án", "Phòng ban 64", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("ee746cd0-471a-48e8-80fb-a755609ac3a8"), new DateOnly(2025, 1, 2), "ĐN-42", 1, "A-2025-8", "VB-42", new DateTime(2025, 2, 19, 13, 43, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo đánh giá hiệu quả công việc", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo đánh giá hiệu quả công việc", 1, 5, 0, null, "Nội dung: Báo cáo đánh giá hiệu quả công việc", "Phòng ban 42", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateOnly(2025, 1, 2), "ĐN-5", 1, "A-2025-1", "VB-5", new DateTime(2025, 1, 4, 8, 59, 49, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Biên bản họp đánh giá nhân sự", new Guid("f5e5a5b5-c5d5-45e5-f5a5-b5c5d5e5f5a5"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Biên bản họp đánh giá nhân sự", 1, 5, 0, null, "Nội dung: Biên bản họp đánh giá nhân sự", "Phòng ban 5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("fa336e0c-087c-469b-8205-40735fab81ed"), new DateOnly(2025, 1, 2), "ĐN-74", 1, "A-2025-13", "VB-74", new DateTime(2025, 4, 22, 19, 38, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Tờ trình xin phê duyệt dự án", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 2, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Tờ trình xin phê duyệt dự án", 1, 5, 0, null, "Nội dung: Tờ trình xin phê duyệt dự án", "Phòng ban 74", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("fbb29d67-b835-4772-9616-23dad7b73227"), new DateOnly(2025, 1, 2), "ĐN-67", 1, "A-2025-12", "VB-67", new DateTime(2025, 3, 14, 11, 58, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Báo cáo tổng kết công tác năm 2024", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Báo cáo tổng kết công tác năm 2024", 1, 5, 0, null, "Nội dung: Báo cáo tổng kết công tác năm 2024", "Phòng ban 67", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("fc630beb-b081-4b1b-aed3-3703614436d3"), new DateOnly(2025, 1, 2), "ĐN-19", 1, "A-2025-4", "VB-19", new DateTime(2025, 4, 23, 21, 35, 21, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Quyết định xử lý kỷ luật", new Guid("e4d4f4a4-b4c4-44d4-e4f4-a4b4c4d4e4f4"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Quyết định xử lý kỷ luật", 1, 5, 0, null, "Nội dung: Quyết định xử lý kỷ luật", "Phòng ban 19", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("fc75a507-f3ac-4d6b-8301-b7ec389d097d"), new DateOnly(2025, 1, 2), "ĐN-47", 1, "A-2025-8", "VB-47", new DateTime(2025, 3, 31, 0, 30, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Thông báo thay đổi lịch làm việc", new Guid("c2b2d2e2-f2a2-42b2-c2d2-e2f2a2b2c2d2"), 0, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Thông báo thay đổi lịch làm việc", 1, 5, 0, null, "Nội dung: Thông báo thay đổi lịch làm việc", "Phòng ban 47", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateOnly(2025, 1, 2), "ĐN-8", 1, "A-2025-2", "VB-8", new DateTime(2025, 4, 23, 12, 50, 57, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Mô tả: Thông báo tuyển dụng nhân sự", new Guid("d3c3e3f3-a3b3-43c3-d3e3-f3a3b3c3d3e3"), 3, new DateOnly(2025, 1, 15), 1, false, 1, new DateOnly(2025, 1, 1), "Thông báo tuyển dụng nhân sự", 1, 5, 0, null, "Nội dung: Thông báo tuyển dụng nhân sự", "Phòng ban 8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DepartmentId", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, null, false, "Giám đốc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, null, false, "Trợ lý giám đốc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, null, false, "Trưởng phòng kinh doanh", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, null, false, "Nhân viên kinh doanh", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, null, false, "Kế toán trưởng", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, null, false, "Kế toán viên", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 4, null, false, "Trưởng phòng nhân sự", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 4, null, false, "Chuyên viên nhân sự", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 5, null, false, "Nhân viên hành chính", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 6, null, false, "Trưởng phòng IT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 6, null, false, "Lập trình viên", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 6, null, false, "Kỹ thuật hệ thống", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 7, null, false, "Quản lý dự án", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 8, null, false, "Nhân viên marketing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 9, null, false, "Chuyên viên đào tạo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 10, null, false, "Kiểm soát viên nội bộ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateIncomingDocumentRight", "CreateInternalDocumentRight", "CreateOutgoingDocumentRight", "CreatedAt", "CreatedBy", "DigitalCertificate", "Email", "Fullname", "ImageSignature", "InitialConfirmProcessRight", "IsDeleted", "ManageCategories", "PasswordHash", "PositionId", "ProcessManagerRight", "RoleId", "StoreDocumentRight", "UpdatedAt", "UpdatedBy", "Username", "WalletAddress" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), true, true, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx", "thanhhungst314@gmail.com", "Nguyễn Thu Trang", "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620976/signature03_t5o01k.png", true, false, true, "C40D0CF1F0815D27829F76BA3F7B0399A9FF5BD6C05252B7F500B6826419EE25-E41A6B82F54C202A240A483B224F15C3", 2, true, 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "user001", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), false, false, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx", "thanh.hung.st302@gmail.com", "Nguyễn Thành Hưng", "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620976/signature02_vv1beq.png", false, false, false, "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F", 2, true, 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "user002", null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), false, false, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx", "user03@gmail.com", "Doãn Nhật Anh", "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620975/signature_01_jkvh5v.png", false, false, false, "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F", 2, false, 3, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "user003", null },
                    { new Guid("3f8b2a1e-5c4d-4e9f-a2b3-7c8d9e0f1a2b"), false, false, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx", "admin@docmino.com", "Admin", "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620976/signature03_t5o01k.png", false, false, false, "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F", 1, false, 1, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "admin", null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), false, false, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx", "user04@gmail.com", "Doãn Nhật Đức", "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620975/signature_01_jkvh5v.png", false, false, false, "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F", 2, false, 3, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "user004", null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), false, false, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx", "user05@gmail.com", "Doãn Nhật Hiếu", "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620975/signature_01_jkvh5v.png", false, false, false, "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F", 2, false, 3, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "user005", null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), false, false, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx", "user06@gmail.com", "Vũ Trung Anh", "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620975/signature_01_jkvh5v.png", false, false, false, "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F", 2, false, 3, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "user006", null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), false, false, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://res.cloudinary.com/dpmjdyter/raw/upload/v1748667753/public_key_cert_onhkmr.pfx", "user06@gmail.com", "Lê Quốc Việt", "https://res.cloudinary.com/dpmjdyter/image/upload/v1748620975/signature_01_jkvh5v.png", false, false, false, "62D97E720D5574BBEB80B41144D1BC86648C78D747DDD4078C62E1E279B4D94D-F75BF08670B03F19CABE8AAD26B5763F", 2, false, 3, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "user007", null }
                });

            migrationBuilder.InsertData(
                table: "ConfirmProcess",
                columns: new[] { "Id", "BlockchainEnabled", "CreatedAt", "CreatedBy", "CurrentStepNumber", "Description", "DocumentId", "IsDeleted", "ManagerId", "Name", "Status", "Type", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "A secure process for high-value document approvals.", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), false, new Guid("11111111-1111-1111-1111-111111111111"), "Secure Approval Process", 0, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Standard review process for regular documents.", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), false, new Guid("11111111-1111-1111-1111-111111111111"), "Normal Review Process", 0, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Process for handling important contracts with blockchain logging.", new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), false, new Guid("11111111-1111-1111-1111-111111111111"), "Important Contract Process", 0, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "GroupId", "UserId", "GroupRole" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), 0 },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111111"), 0 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111111"), 0 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("55555555-5555-5555-5555-555555555555"), 0 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("44444444-4444-4444-4444-444444444444"), 0 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("55555555-5555-5555-5555-555555555555"), 0 }
                });

            migrationBuilder.InsertData(
                table: "ProcessDetail",
                columns: new[] { "Id", "ActionName", "DateEnd", "DateStart", "ProcessId", "ResignDateEnd", "ReviewerDepartmentId", "ReviewerGroupId", "ReviewerName", "ReviewerPositionId", "ReviewerType", "ReviewerUserId", "SignType", "StepNumber", "VetoRight" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateOnly(2025, 1, 8), new DateOnly(2025, 1, 1), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateOnly(2025, 1, 11), null, null, null, 1, 2, null, 0, 1, false },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateOnly(2025, 1, 15), new DateOnly(2025, 1, 9), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateOnly(2025, 1, 18), null, null, null, null, 0, new Guid("11111111-1111-1111-1111-111111111111"), 1, 2, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmProcess_DocumentId",
                table: "ConfirmProcess",
                column: "DocumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmProcess_ManagerId",
                table: "ConfirmProcess",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Id0",
                table: "Department",
                column: "Id0",
                unique: true,
                filter: "[Id0] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Document_CategoryId",
                table: "Document",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DepartmentId",
                table: "Document",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentRegisterId",
                table: "Document",
                column: "DocumentRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_FieldId",
                table: "Document",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_OrganizationId",
                table: "Document",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_StorageId",
                table: "Document",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDirectory_ParentDirectoryId",
                table: "DocumentDirectory",
                column: "ParentDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFile_DocumentId",
                table: "DocumentFile",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_DepartmentId",
                table: "Position",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDetail_ProcessId",
                table: "ProcessDetail",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDetail_ReviewerDepartmentId",
                table: "ProcessDetail",
                column: "ReviewerDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDetail_ReviewerGroupId",
                table: "ProcessDetail",
                column: "ReviewerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDetail_ReviewerPositionId",
                table: "ProcessDetail",
                column: "ReviewerPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDetail_ReviewerUserId",
                table: "ProcessDetail",
                column: "ReviewerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessHistory_ProcessId",
                table: "ProcessHistory",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessHistory_UserReviewerId",
                table: "ProcessHistory",
                column: "UserReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessSignDetail_FileId",
                table: "ProcessSignDetail",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessSignDetail_ProcessDetailsId",
                table: "ProcessSignDetail",
                column: "ProcessDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessSignHistory_OriginalFileId",
                table: "ProcessSignHistory",
                column: "OriginalFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessSignHistory_ProcessHistoryId",
                table: "ProcessSignHistory",
                column: "ProcessHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Storage_Code",
                table: "Storage",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Storage_DirectoryId",
                table: "Storage",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Storage_StoragePeriodId",
                table: "Storage",
                column: "StoragePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PositionId",
                table: "User",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeature_FeatureId",
                table: "UserFeature",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId",
                table: "UserGroup",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessSignDetail");

            migrationBuilder.DropTable(
                name: "ProcessSignHistory");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "SystemMenus");

            migrationBuilder.DropTable(
                name: "UserFeature");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "ProcessDetail");

            migrationBuilder.DropTable(
                name: "DocumentFile");

            migrationBuilder.DropTable(
                name: "ProcessHistory");

            migrationBuilder.DropTable(
                name: "SystemFeature");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "ConfirmProcess");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "DocumentCategory");

            migrationBuilder.DropTable(
                name: "DocumentField");

            migrationBuilder.DropTable(
                name: "DocumentRegister");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "DocumentDirectory");

            migrationBuilder.DropTable(
                name: "StoragePeriod");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
