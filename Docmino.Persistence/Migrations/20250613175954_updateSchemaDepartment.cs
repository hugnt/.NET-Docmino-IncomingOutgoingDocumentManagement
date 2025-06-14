using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Docmino.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateSchemaDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Department_Id0",
                table: "Department");

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9464), new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9472) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9491) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9502), new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9502) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9511), new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9511) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9525), new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9525) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9540), new DateTime(2025, 6, 13, 17, 59, 49, 553, DateTimeKind.Utc).AddTicks(9541) });

            migrationBuilder.CreateIndex(
                name: "IX_Department_Id0",
                table: "Department",
                column: "Id0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Department_Id0",
                table: "Department");

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5054), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5059) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5081), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5082) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5103), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5104) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5111), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5112) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5118), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5119) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5129), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5130) });

            migrationBuilder.CreateIndex(
                name: "IX_Department_Id0",
                table: "Department",
                column: "Id0",
                unique: true,
                filter: "[Id0] IS NOT NULL");
        }
    }
}
