using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Docmino.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateConfigurationOfUserGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_Group_UserId",
                table: "UserGroup");

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(782), new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(786) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(795), new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(796) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(802), new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(802) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(807), new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(808) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(813), new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(813) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(828), new DateTime(2025, 6, 13, 19, 45, 29, 432, DateTimeKind.Utc).AddTicks(828) });

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_Group_GroupId",
                table: "UserGroup",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_Group_GroupId",
                table: "UserGroup");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_Group_UserId",
                table: "UserGroup",
                column: "UserId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
